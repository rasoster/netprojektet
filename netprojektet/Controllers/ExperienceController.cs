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
            ExperienceViewModel experience = new ExperienceViewModel();
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");

            return View(experience);
        }
        //Lägger till ny erfarenhet
        [HttpPost]
        public IActionResult AddExperience (ExperienceViewModel newExperience)
        {

            linkedoutDbContext.Add(newExperience.experience);
            linkedoutDbContext.SaveChanges();

            ProfileHasExperience profileinExperience = new ProfileHasExperience();
            profileinExperience.Startdate = newExperience.profileHasExperience.Startdate;
            profileinExperience.Enddate = newExperience.profileHasExperience.Enddate;

            profileinExperience.Experience = linkedoutDbContext.Experiences.Find(newExperience.experience.Id);

            profileinExperience.Profile = (from p in linkedoutDbContext.Profiles
                                          where p.UserName == User.Identity.Name
                                          select p).FirstOrDefault();

            profileinExperience.Experienceid = newExperience.experience.Id;
            profileinExperience.Profileid = profileinExperience.Profile.Id;
            linkedoutDbContext.ProfileHasExperiences.Add(profileinExperience);
            linkedoutDbContext.SaveChanges();


            Profile profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            return RedirectToAction("Profile", "Profile", new { profileID = profile.Id });
        }
        [HttpGet]
        public IActionResult RemoveExperience(int experienceID)
        {
            Experience experience = linkedoutDbContext.Experiences.Find(experienceID);
            return View(experience);
        }
        [HttpPost]
        public IActionResult RemoveExperience(Experience experience)
        {
            List<ProfileHasExperience> profilehasexperience = linkedoutDbContext.ProfileHasExperiences.Where(e => e.Experienceid == experience.Id).ToList();

            int profileid = (from p in linkedoutDbContext.Profiles
                             where p.UserName == User.Identity.Name
                             select p.Id).FirstOrDefault();

            if (profilehasexperience.Count() == 1 && profilehasexperience[0].Profileid == profileid)
            {
                linkedoutDbContext.ProfileHasExperiences.Remove(profilehasexperience[0]);
                linkedoutDbContext.Experiences.Remove(experience);
                linkedoutDbContext.SaveChanges();
            }
            else if (profilehasexperience.Count() > 1)
            {
                foreach (ProfileHasExperience item in profilehasexperience)
                {
                    if (item.Profileid == profileid)
                    {
                        linkedoutDbContext.ProfileHasExperiences.Remove(item);
                        linkedoutDbContext.SaveChanges();

                    }
                }
            }


            return RedirectToAction("Profile", "Profile", new { profileID = profileid });

        }
    }
}