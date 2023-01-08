using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace netprojektet.Controllers
{
    public class CompetenceController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public CompetenceController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }
        
        //startar ny kompetens formuläret
        [HttpGet]
        public IActionResult addCompetence()
        {
            CompetenceViewModel competence = new CompetenceViewModel();
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
           
            return View(competence);
        }
        //Lägger till ny kompetens
        [HttpPost]
        public IActionResult AddCompetence(CompetenceViewModel model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            }
            Competence newCompetence = new Competence();
            newCompetence.Name = model.Name;
            newCompetence.Description = model.Description;


            linkedoutDbContext.Add(newCompetence);
            linkedoutDbContext.SaveChanges();


            

            ProfileHasCompetence profileHasCompetence = new ProfileHasCompetence();


            profileHasCompetence.Competence = newCompetence;

            profileHasCompetence.Profile = (from p in linkedoutDbContext.Profiles
                                        where p.UserName == User.Identity.Name
                                        select p).FirstOrDefault();

            profileHasCompetence.Competenceid = newCompetence.Id;
            profileHasCompetence.Profileid = profileHasCompetence.Profile.Id;
            linkedoutDbContext.ProfileHasCompetences.Add(profileHasCompetence);
            linkedoutDbContext.SaveChanges();
            
            Profile profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            return RedirectToAction("Profile", "Profile" , new { profileID = profile.Id });
        }
        //startar ta bort kompetens formuläret
        [HttpGet]
        public IActionResult RemoveCompetence(int competenceID)
        {
            Competence competence = linkedoutDbContext.Competences.Find(competenceID);
            return View(competence);
        }
        //Tar bort kompetens
        [HttpPost]
        public IActionResult RemoveCompetence(Competence competence)
        {
 
            List<ProfileHasCompetence> profileHasCompetence = linkedoutDbContext.ProfileHasCompetences.Where(e => e.Competenceid == competence.Id).ToList();

            int profileid = (from p in linkedoutDbContext.Profiles
                            where p.UserName == User.Identity.Name
                            select p.Id).FirstOrDefault();

            //Tar först bort sambandsrader för vald utbildning sedan tas utbildningen bort i utbildningstabellen om endast en användare har denna utbildning
            if (profileHasCompetence.Count()==1 && profileHasCompetence[0].Profileid==profileid)
            {
                linkedoutDbContext.ProfileHasCompetences.Remove(profileHasCompetence[0]);
                linkedoutDbContext.Competences.Remove(competence);
                linkedoutDbContext.SaveChanges();
            }else if (profileHasCompetence.Count() > 1)
            {
                foreach (ProfileHasCompetence item in profileHasCompetence)
                {
                    //Tar bara bort objekt från sambandstabell för aktiv användare 
                    if (item.Profileid == profileid)
                    {
                        linkedoutDbContext.ProfileHasCompetences.Remove(item);
                        linkedoutDbContext.SaveChanges();

                    }
                }
            }
            
            
            return RedirectToAction("Profile", "Profile", new { profileID = profileid });

        }
    }
}



