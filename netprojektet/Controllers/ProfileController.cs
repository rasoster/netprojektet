using Azure.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netprojektet.Models.DataLayer;

namespace netprojektet.Controllers
{
    public class ProfileController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public ProfileController (LinkedoutDbContext DbContext)
        {
           linkedoutDbContext = DbContext;
        }
        [HttpGet]
        public IActionResult Profile(int profileID)
        {
            Profile myProfile = linkedoutDbContext.Profiles.Find(profileID);
            if (profileID == -1) 
            { 
                myProfile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
                
            }
            
            return View(myProfile);

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
            linkedoutDbContext.Add(newProfile);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Profile", "Home");


        }
        [HttpGet]
        public IActionResult UpdateProfile(int profileID)
        {
            Profile profile = linkedoutDbContext.Profiles.Find(profileID);
            return View(profile);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile uppdatedProfile)
        {
            linkedoutDbContext.Profiles.Update(uppdatedProfile);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Profile");

        }
    }
}
