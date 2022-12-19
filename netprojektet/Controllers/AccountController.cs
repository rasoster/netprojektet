using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            if(!ModelState.IsValid)
            {
                Anvandare anvandare = new Anvandare();
                anvandare.UserName = registerViewModel.UserName;

               
                var result = await userManager.CreateAsync(anvandare,registerViewModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(anvandare, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
               

            }
                return View(registerViewModel);
            
        }
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
