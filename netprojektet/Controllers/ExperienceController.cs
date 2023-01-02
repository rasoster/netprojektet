using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace netprojektet.Controllers
{
    public class ExperienceController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public ExperienceController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        //startar ny utbildning formuläret
        [HttpGet]
        public IActionResult addExperience()
        {
            Experience experience = new Experience();

            return View(experience);
        }
        //Lägger till ny erfarenhet
        [HttpPost]
        public IActionResult AddExperience (Experience newExperience)
        {

            linkedoutDbContext.Add(newExperience);
            linkedoutDbContext.SaveChanges();

            ProfileHasExperience profileinExperience = new ProfileHasExperience();

            profileinExperience.Experience = linkedoutDbContext.Experiences.Find(newExperience.Id);

            profileinExperience.Profile = (from p in linkedoutDbContext.Profiles
                                          where p.UserName == User.Identity.Name
                                          select p).FirstOrDefault();

            profileinExperience.Experienceid = newExperience.Id;
            profileinExperience.Profileid = profileinExperience.Profile.Id;
            linkedoutDbContext.ProfileHasExperiences.Add(profileinExperience);
            linkedoutDbContext.SaveChanges();


            Profile profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            return RedirectToAction("Profile", "Profile", new { profileID = profile.Id });
        }
    }
}