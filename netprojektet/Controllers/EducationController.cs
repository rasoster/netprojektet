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
            EducationViewModel education = new EducationViewModel();
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
           
            return View(education);
        }
        //Lägger till ny Utbildning
        [HttpPost]
        public IActionResult AddEducation(EducationViewModel newEducation)
        {


            linkedoutDbContext.Add(newEducation.education);
            linkedoutDbContext.SaveChanges();
            

            ProfileHasEducation profileinEducation = new ProfileHasEducation();
            profileinEducation.Startdate = newEducation.profileHasEducation.Startdate;
            profileinEducation.Enddate = newEducation.profileHasEducation.Enddate;

            profileinEducation.Education = linkedoutDbContext.Educations.Find(newEducation.education.Id);

            profileinEducation.Profile = (from p in linkedoutDbContext.Profiles
                                        where p.UserName == User.Identity.Name
                                        select p).FirstOrDefault();

            profileinEducation.Educationid = newEducation.education.Id;
            profileinEducation.Profileid = profileinEducation.Profile.Id;
            linkedoutDbContext.ProfileHasEducations.Add(profileinEducation);
            linkedoutDbContext.SaveChanges();
            
            Profile profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            return RedirectToAction("Profile", "Profile" , new { profileID = profile.Id });
        }
        //startar ta bort utbildning formuläret
        [HttpGet]
        public IActionResult RemoveEducation(int educationID)
        {
            Education education = linkedoutDbContext.Educations.Find(educationID);
            return View(education);
        }
        //Tar bort utbildning
        [HttpPost]
        public IActionResult RemoveEducation(Education education)
        {
 
            List<ProfileHasEducation> profilehaseducation = linkedoutDbContext.ProfileHasEducations.Where(e => e.Educationid == education.Id).ToList();

            int profileid = (from p in linkedoutDbContext.Profiles
                            where p.UserName == User.Identity.Name
                            select p.Id).FirstOrDefault();

            //Tar först bort sambandsrader för vald utbildning sedan tas utbildningen bort i utbildningstabellen om endast en användare har denna utbildning
            if (profilehaseducation.Count()==1 && profilehaseducation[0].Profileid==profileid)
            {
                linkedoutDbContext.ProfileHasEducations.Remove(profilehaseducation[0]);
                linkedoutDbContext.Educations.Remove(education);
                linkedoutDbContext.SaveChanges();
            }else if (profilehaseducation.Count() > 1)
            {
                foreach (ProfileHasEducation item in profilehaseducation)
                {
                    //Tar bara bort objekt från sambandstabell för aktiv användare 
                    if (item.Profileid == profileid)
                    {
                        linkedoutDbContext.ProfileHasEducations.Remove(item);
                        linkedoutDbContext.SaveChanges();

                    }
                }
            }
            
            
            return RedirectToAction("Profile", "Profile", new { profileID = profileid });

        }
    }
}



