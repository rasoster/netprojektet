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
            List<Profile> profileListFull = linkedoutDbContext.Profiles.ToList();
            List<Profile> profileListLimited = linkedoutDbContext.Profiles.Where(e => e.Private == false).ToList();
            Project projekt = linkedoutDbContext.Projects.Where(e => e.Creator.Private == false).OrderByDescending(e => e.Id).FirstOrDefault();
            //om användaren är inloggad får hen hela listan, annars en begränsad.
            model.senasteProject = projekt;
            model.experiences = linkedoutDbContext.ProfileHasExperiences.ToList();
            model.educations = linkedoutDbContext.ProfileHasEducations.ToList();
            model.profileInProject = linkedoutDbContext.ProfileinProjects.ToList();
            if (User.Identity.IsAuthenticated)
            {
                model.profiles = profileListFull;
            }
            else { 
            model.profiles = profileListLimited;
            }
            return View(model);
        }
      
        [HttpPost]
        public IActionResult Search()
        {//delar upp alla ord i frågan till en lista.
            string searchQuery = Request.Form["Query"].ToString();
            var searchQuerys = searchQuery.Split(" ");
            List<Profile> results = new List<Profile>();
            List<Profile> profiles = new List<Profile>();
            //om inget skrivet i sökfältet får man hela sökfältet igen.
            if(searchQuery == null)
            {
                return RedirectToAction("Index");
            }
            //om frågan innehåller ett ord får man resultat där ordet förekommer i förnamn eller efternamn
            if(searchQuerys.Length == 1) {
                
                profiles = linkedoutDbContext.Profiles.Where(p => p.FirstName.Contains(searchQuery) || p.LastName.Contains(searchQuery)).ToList();
            
            }
            //om frågan innehåller två ord får man resultat där första ordet finns i förnamnet och andra ordet i efternamnet
            //eller tvärt om att första ordet finns i efternamnet och andra ordet i förnamnet.
            if(searchQuerys.Length == 2)
            {

                foreach (var query in searchQuerys)
                {

                    List<Profile> queryResults = linkedoutDbContext.Profiles.Where(p => p.FirstName.Contains(searchQuerys[0]) && p.LastName.Contains(searchQuerys[1])
                                                    || p.LastName.Contains(searchQuerys[0]) && p.FirstName.Contains(searchQuerys[1])).ToList();

                    foreach(Profile queryProfile in queryResults)
                    {
                        profiles.Add(queryProfile);
                    }

                }

            }
            //om det är fler är två ord tar den fram resultat där något av orden finns i antingen förnamn eller efternamn
            else
            {
                foreach (var query in searchQuerys)
                {


                    List<Profile> queryResults = linkedoutDbContext.Profiles
                             .Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query)).ToList();

                    foreach (Profile queryProfile in queryResults)
                    {
                        profiles.Add(queryProfile);
                    }

                }

            }
            //dublettkontroll
            foreach (Profile profile in profiles)
            {
                if (results.Contains(profile))
                {

                }
                else
                {
                    results.Add(profile);
                }
            }
            List<Profile> filteredList = new List<Profile>();
            if (User.Identity.IsAuthenticated)
            {
                filteredList = results;
            }
            else
            {
               filteredList = results.Where(p => p.Private == false).ToList();
            }
            //lägger in allt i en viewModel
            ProfileProjectViewModel profileProjectViewModel = new ProfileProjectViewModel();
            profileProjectViewModel.profiles = filteredList;
            profileProjectViewModel.experiences = linkedoutDbContext.ProfileHasExperiences.ToList();
            profileProjectViewModel.educations = linkedoutDbContext.ProfileHasEducations.ToList();
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