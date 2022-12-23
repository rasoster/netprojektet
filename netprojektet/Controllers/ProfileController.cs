using Azure.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccessLayer;

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
            ProfileViewModel profileViewModel = new ProfileViewModel();
            
            if (profileID == -1) 
            { 
                profileViewModel.profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
                
            }
            else
            {
                profileViewModel.profile = linkedoutDbContext.Profiles.Find(profileID);
            }
            profileViewModel.profileHasEducation = linkedoutDbContext.ProfileHasEducations.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();
            profileViewModel.profileHasExperience = linkedoutDbContext.ProfileHasExperiences.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();
            profileViewModel.profileinProject = linkedoutDbContext.ProfileinProjects.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();
            profileViewModel.project = linkedoutDbContext.Projects.ToList();
            profileViewModel.Experience = linkedoutDbContext.Projects.ToList();
            profileViewModel.Education = linkedoutDbContext.Projects.ToList();



            return View(profileViewModel);

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
            uppdatedProfile.UserName = User.Identity.Name;
            linkedoutDbContext.Profiles.Update(uppdatedProfile);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Profile");
            

        }
    }
}
