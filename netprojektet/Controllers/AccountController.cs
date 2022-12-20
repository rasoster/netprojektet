using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using netprojektet.Models.DataLayer;
using netprojektet.Models.ViewModels;

namespace netprojektet.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Anvandare> userManager;
        private SignInManager<Anvandare> signInManager;

        public AccountController(UserManager<Anvandare> userManager, SignInManager<Anvandare> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                Anvandare anvandare = new Anvandare();
                anvandare.UserName = registerViewModel.UserName;

               
                var result = await userManager.CreateAsync(anvandare,registerViewModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(anvandare, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
                return View(registerViewModel);
            
        }

       [HttpPost]
       public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, isPersistent: loginViewModel.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded) { 
                return RedirectToAction("Index", "Home");
                }
            }
            return View(loginViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
