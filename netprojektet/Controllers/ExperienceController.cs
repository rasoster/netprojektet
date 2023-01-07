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
        public IActionResult AddExperience (ExperienceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Experience newExperience = new Experience();
            newExperience.Name = model.Name;
            newExperience.Description = model.Description;

            linkedoutDbContext.Add(newExperience);
            linkedoutDbContext.SaveChanges();

            ProfileHasExperience profileinExperience = new ProfileHasExperience();
            profileinExperience.Startdate = model.Startdate;
            profileinExperience.Enddate = model.Enddate;

            profileinExperience.Experience = newExperience;

            profileinExperience.Profile = (from p in linkedoutDbContext.Profiles
                                          where p.UserName == User.Identity.Name
                                          select p).FirstOrDefault();

            profileinExperience.Experienceid = newExperience.Id;
            profileinExperience.Profileid = profileinExperience.Profile.Id;
            linkedoutDbContext.ProfileHasExperiences.Add(profileinExperience);
            linkedoutDbContext.SaveChanges();


            
            return RedirectToAction("Profile", "Profile", new { profileID = profileinExperience.Profileid });
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

            //Tar först bort sambandsrader för vald erfarenhet sedan tas erfarenheten bort i erfarenhetsstabellen om endast en användare har denna utbildning
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
                    //Tar bara bort erfarenhet från sambandstabell för aktiv användare 
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