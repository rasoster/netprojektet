using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace netprojektet.Controllers
{
    public class EducationController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public EducationController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        //startar ny utbildning formuläret
        [HttpGet]
        public IActionResult addEducation()
        {
            Education education = new Education();
           
            return View(education);
        }
        //Lägger till ny Utbildning
        [HttpPost]
        public IActionResult AddEducation(Education newEducation)
        {


            linkedoutDbContext.Add(newEducation);
            linkedoutDbContext.SaveChanges();
            

            ProfileHasEducation profileinEducation = new ProfileHasEducation();

            profileinEducation.Education = linkedoutDbContext.Educations.Find(newEducation.Id);

            profileinEducation.Profile = (from p in linkedoutDbContext.Profiles
                                        where p.UserName == User.Identity.Name
                                        select p).FirstOrDefault();

            profileinEducation.Educationid = newEducation.Id;
            profileinEducation.Profileid = profileinEducation.Profile.Id;
            linkedoutDbContext.ProfileHasEducations.Add(profileinEducation);
            linkedoutDbContext.SaveChanges();
            
            Profile profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            return RedirectToAction("Profile", "Profile" , new { profileID = profile.Id });
        }
    }
}



