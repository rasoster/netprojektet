using Azure.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccessLayer;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text.Json;
using netprojektet.Migrations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Serialization;

namespace netprojektet.Controllers
{
    public class ProfileController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;
        private HttpClient httpClient;
       

        public ProfileController(LinkedoutDbContext DbContext, HttpClient httpClient)
        {
            linkedoutDbContext = DbContext;
            this.httpClient = httpClient; 
            
        }
        //tar in profilID och tar fram den profil som ska visas.
        [HttpGet]
        public async Task<IActionResult> Profile(int profileID)
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";
            Profile profile = linkedoutDbContext.Profiles.Find(profileID);
            if (!User.Identity.IsAuthenticated && profile.Private == true)
            {
                return RedirectToAction("index", "home");
            }

            ProfileViewModel profileViewModel = await fillViewModel(profileID);
            return View(profileViewModel);
        }
        public async Task<ProfileViewModel> fillViewModel(int profileID)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();

            //om man klickar på "min profil" skickas värdet -1 för att sedan ersättas med rätt värde med hjälp av user.Identity
            if (profileID == -1 && User.Identity.IsAuthenticated)
            {

                profileViewModel.profile = linkedoutDbContext.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
                HttpResponseMessage response = await httpClient.GetAsync($"Profile/{profileViewModel.profile.Id}");

                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Profile myProfile = System.Text.Json.JsonSerializer.Deserialize<Profile>(data, options);
                ViewBag.Visitors = "Du har haft " + myProfile.Visitors + " besökare på din sida";
            }


            //annars används profilID parametern
            else
            {
                profileViewModel.profile = linkedoutDbContext.Profiles.Find(profileID);
            }

            httpClient.DefaultRequestHeaders.Add("User-Agent", "rasoster");
            
            HttpResponseMessage gitResponse = await httpClient.GetAsync("https://api.github.com/users/karpathy/repos");

            string gitData = await gitResponse.Content.ReadAsStringAsync();


            profileViewModel.gitHubRepository = JsonConvert.DeserializeObject<List<GitHubRepository>>(gitData);


            profileViewModel.profileHasEducation = linkedoutDbContext.ProfileHasEducations.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();
            profileViewModel.profileHasExperience = linkedoutDbContext.ProfileHasExperiences.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();

            profileViewModel.profileinProject = linkedoutDbContext.ProfileinProjects.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();


            List<ProfileinProject> profileinProjects = (from p in linkedoutDbContext.ProfileinProjects
                                                        join o in linkedoutDbContext.Anvandares on p.Profile.UserName equals o.UserName
                                                        where p.Profileid == profileViewModel.profile.Id
                                                        select p).ToList();
            profileViewModel.profileinProject = (from p in profileinProjects
                                                 join o in linkedoutDbContext.Anvandares on p.Project.Creator.UserName equals o.UserName
                                                 where o.LockoutEnabled == false
                                                 select p).ToList();
            profileViewModel.profileHasCompetence = linkedoutDbContext.ProfileHasCompetences.Where(e => e.Profileid == profileViewModel.profile.Id).ToList();


            foreach (ProfileinProject profileinProject in profileViewModel.profileinProject)
            {
                List<ProfileinProject> result = (from p in linkedoutDbContext.ProfileinProjects
                                                 where p.Projectid == profileinProject.Projectid && p.Profileid != profileViewModel.profile.Id
                                                 select p).ToList();
                if (result.Count() > 0)
                {
                    foreach (ProfileinProject item in result)
                    {
                        profileViewModel.similarProject.Add(item.Profile);
                    }
                }

            }

            return profileViewModel;

        }
        //public async Task<int> getVisitors(int profileID)
        //{
        //    HttpResponseMessage response = await httpClient.GetAsync($"Profile/{profileID}");

        //    string data = await response.Content.ReadAsStringAsync();
        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true,
        //    };
        //    Profile myProfile = JsonSerializer.Deserialize<Profile>(data, options);

        //    return myProfile.Visitors;
        //}





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
           
            if (!ModelState.IsValid)
            {
                return View(newProfile);
            }
            Profile theProfile = linkedoutDbContext.Profiles.FirstOrDefault(e => e.UserName == User.Identity.Name);
            
            
            theProfile.PicUrl = "/Content.Images.DefaultProfilePic.png";
            theProfile.FirstName = newProfile.FirstName;
            theProfile.LastName = newProfile.LastName;
            theProfile.Private = newProfile.Private;
            theProfile.Email = newProfile.Email;
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
            if (!ModelState.IsValid)
            {
                return View(uppdatedProfile);
            }
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
        public async Task<IActionResult> CreateXml(int profileid)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content");
            ProfileViewModel model = await fillViewModel(profileid);

            XmlSerializer xmlSerializer= new XmlSerializer(typeof(ProfileViewModel));
            using (StreamWriter writer = new System.IO.StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, model);
            }
            return RedirectToAction("Profile", new { profileID = profileid });

        }



    }
}
