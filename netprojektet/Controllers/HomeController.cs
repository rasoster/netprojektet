using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace netprojektet.Controllers
{
    public class HomeController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;


        public HomeController(LinkedoutDbContext DbContext)
        {
            linkedoutDbContext = DbContext;
        }

        
        public IActionResult Index()
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";
            var model = new ProfileProjectViewModel();
            //lägger till en lista i viewModel baserat på om profilerna är privata eller inte
            List<Profile> profiles = new List<Profile>();
            if (User.Identity.IsAuthenticated)
            {
                profiles = (from p in linkedoutDbContext.Profiles
                                          join o in linkedoutDbContext.Anvandares on p.UserName equals o.UserName
                                          where o.LockoutEnabled == false
                                          select p).ToList();
                                         

            }
            else
            {
                profiles = (from p in linkedoutDbContext.Profiles
                                          join o in linkedoutDbContext.Anvandares on p.UserName equals o.UserName
                                          where o.LockoutEnabled == false && p.Private == false
                                          select p).ToList();
            }
            model.profiles = profiles;
            Project projekt = linkedoutDbContext.Projects.Where(e => e.Creator.Private == false).OrderByDescending(e => e.Id).FirstOrDefault();
            //om användaren är inloggad får hen hela listan, annars en begränsad.
            model.senasteProject = projekt;
            model.experiences = linkedoutDbContext.ProfileHasExperiences.ToList();
            model.educations = linkedoutDbContext.ProfileHasEducations.ToList();
            model.competences= linkedoutDbContext.ProfileHasCompetences.ToList();
            model.profileInProject = linkedoutDbContext.ProfileinProjects.ToList();

            return View(model);
        }
      
        [HttpPost]
        public IActionResult Search()
        {//delar upp alla ord i frågan till en lista.
            string searchQuery = Request.Form["Query"].ToString();
            var searchQuerys = searchQuery.Split(" ");
            List<Profile> profiles = new List<Profile>();
            //om inget skrivet i sökfältet får man hela sökfältet igen.
            if(searchQuery == null)
            {
                return RedirectToAction("Index");
            }
            //om frågan innehåller ett ord får man resultat där ordet förekommer i förnamn eller efternamn
           

                foreach (var query in searchQuerys)
                {

                List<Profile> queryResults = linkedoutDbContext.Profiles.Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query)).ToList();

                //Kontrollerar om sökordet finns i någon kompetens
                List<Profile> profileCompetence = (from p in linkedoutDbContext.ProfileHasCompetences
                                                   where p.Competence.Name.Contains(query)
                                                   select p.Profile).ToList();

                List<Profile> results = queryResults.Union(profileCompetence).ToList();
                
                    foreach (Profile queryProfile in results)
                    {
                        profiles.Add(queryProfile);
                    }

                }
            //rensar ut dubletter
            List<Profile> noDupProfiles = new List<Profile>();
            foreach (Profile profile in profiles)
            {
                if (noDupProfiles.Contains(profile))
                {

                }
                else
                {
                    noDupProfiles.Add(profile);
                }
            }
            //tilldelar profiler baserat på om användaren är inloggad
            List<Profile> filteredList = new List<Profile>();
            if (User.Identity.IsAuthenticated)
            {
                filteredList = (from r in noDupProfiles
                                join o in linkedoutDbContext.Anvandares on r.UserName equals o.UserName
                               where o.LockoutEnabled == false
                               select r).ToList();
                               
                               
            }
            else
            {
               filteredList = (from r in noDupProfiles
                               join o in linkedoutDbContext.Anvandares on r.UserName equals o.UserName
                               where o.LockoutEnabled == false && r.Private == false
                               select r).ToList();

            }
            //lägger in allt i en viewModel
            ProfileProjectViewModel profileProjectViewModel = new ProfileProjectViewModel();
            profileProjectViewModel.profiles = filteredList;
            profileProjectViewModel.experiences = linkedoutDbContext.ProfileHasExperiences.ToList();
            profileProjectViewModel.educations = linkedoutDbContext.ProfileHasEducations.ToList();
            profileProjectViewModel.competences = linkedoutDbContext.ProfileHasCompetences.ToList();
            profileProjectViewModel.profileInProject = linkedoutDbContext.ProfileinProjects.ToList();


            return View(profileProjectViewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Projects()
        {
            return RedirectToAction("Project", "Project");
         
        }
    }
}