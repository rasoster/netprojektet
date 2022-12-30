using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

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
        { //kontrollerar om alla fält i ViewModel fyllts it
            if(ModelState.IsValid)
            {
                Anvandare anvandare = new Anvandare();
                anvandare.UserName = registerViewModel.UserName;

               //skapar användare
                var result = await userManager.CreateAsync(anvandare,registerViewModel.Password);
               //om lyckat loggas användare in och hänvisas till skapa profil sidan
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(anvandare, isPersistent: true);
                    return RedirectToAction("RegisterProfile","Profile");
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
            //loggar in användare
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
        {   //loggar ut användare
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
