using Azure.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccessLayer;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace netprojektet.Controllers
{
    public class ProfileController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public ProfileController (LinkedoutDbContext DbContext)
        {
           linkedoutDbContext = DbContext;
        }
        //tar in profilID och tar fram den profil som ska visas.
        [HttpGet]
        public IActionResult Profile(int profileID)
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";

            ProfileViewModel profileViewModel = new ProfileViewModel();
            if (User.Identity.IsAuthenticated) { 
            //om man klickar på "min profil" skickas värdet -1 för att sedan ersättas med rätt värde med hjälp av user.Identity
            if (profileID == -1) 
            { 
                profileViewModel.profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
                
            }
            
            
            //annars används profilID parametern
            else
            {
                profileViewModel.profile = linkedoutDbContext.Profiles.Find(profileID);
            }
            profileViewModel.profileHasEducation = linkedoutDbContext.ProfileHasEducations.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();
            profileViewModel.profileHasExperience = linkedoutDbContext.ProfileHasExperiences.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();
            profileViewModel.profileinProject = linkedoutDbContext.ProfileinProjects.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();


      
           
                return View(profileViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
        //startar registrera profil formuläret
        [HttpGet]
        public IActionResult RegisterProfile()
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";

            return View(new Profile());
        }
        //skickar registrera profil formuläret
        [HttpPost]
        public IActionResult RegisterProfile(Profile newProfile)
        {
            Profile theProfile = linkedoutDbContext.Profiles.FirstOrDefault(e => e.UserName == User.Identity.Name);
            
            theProfile.Visitors = 0;
            theProfile.PicUrl = "/Content.Images.DefaultProfilePic.png";
            theProfile.FirstName = newProfile.FirstName;
            theProfile.LastName = newProfile.LastName;
            theProfile.Private = newProfile.Private;
            linkedoutDbContext.Update(theProfile);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Profile",new {profileId = theProfile.Id});


        }
        //startar uppdatera profil formuläret
        [HttpGet]
        public IActionResult UpdateProfile(int profileID)
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";

            Profile profile = linkedoutDbContext.Profiles.Find(profileID);
            return View(profile);
        }
        //Uppdaterar profilen
        [HttpPost]
        public IActionResult UpdateProfile(Profile uppdatedProfile)
        {
            //Det var problem med foreign key i denna tabell så nedan lösning har implementerats.
            //kopierar över all info från viewModel till den existerande profilen.
            Profile profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            profile.FirstName = uppdatedProfile.FirstName;
            profile.LastName = uppdatedProfile.LastName;
            profile.Email = uppdatedProfile.Email;
            profile.Private = uppdatedProfile.Private;
            linkedoutDbContext.Profiles.Update(profile);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Profile",new {profileID = profile.Id});
            

        }
        [HttpPost]
        public async Task<IActionResult> UploadPic(ProfileViewModel model)
        {
            Profile currentProfile = linkedoutDbContext.Profiles.FirstOrDefault(e => e.UserName == User.Identity.Name);

            try
            { 
           
                string fileName = Path.GetFileName(model.Image.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Content", "images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }
               
            SetPicUrl(currentProfile, fileName);

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Vänligen ange en korrekt bild (filformat .png .jpg och .img accepteras)";
            }

            return RedirectToAction("Profile", new { profileID = currentProfile.Id });
        }
        public void SetPicUrl(Profile currentProfile,string fileName)
        {
            currentProfile.PicUrl = "/Content/Images/" + fileName;
            linkedoutDbContext.Profiles.Update(currentProfile);
            linkedoutDbContext.SaveChanges();
        }



    }
}
