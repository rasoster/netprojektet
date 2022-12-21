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
        private LinkedoutDbContext _dbContext;

        public AccountController(UserManager<Anvandare> userManager, SignInManager<Anvandare> signInManager, LinkedoutDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _dbContext = dbContext;
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
                    return RedirectToAction("RegisterProfile");
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
        [HttpGet]
        public IActionResult RegisterProfile()
        {
            return View(new Profile());
        }
        [HttpPost]
        public IActionResult RegisterProfile(Profile newProfile)
        {

                newProfile.UserName = HttpContext.User.Identity.Name;
                newProfile.Visitors = 0;
                _dbContext.Add(newProfile);
                _dbContext.SaveChanges();
                return RedirectToAction("Profile", "Home");
                
        
        }
    }
}
