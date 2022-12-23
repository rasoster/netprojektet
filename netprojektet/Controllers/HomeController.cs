using Microsoft.AspNetCore.Mvc;
using netprojektet.Models;
using System.Diagnostics;
using netprojektet.Models.DataLayer;
using Microsoft.AspNetCore.Authorization;
using netprojektet.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.ComponentModel;

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
            var model = new ProfileProjectViewModel();
            
            List<Profile> profileListFull = linkedoutDbContext.Profiles.ToList();
            List<Profile> profileListLimited = linkedoutDbContext.Profiles.Where(e => e.Private == false).ToList();
            Project projekt = linkedoutDbContext.Projects.OrderByDescending(e => e.Id).FirstOrDefault();
            
            model.senasteProject = projekt;
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
        {
            string searchQuery = Request.Form["Query"].ToString();
           

            var profiles = linkedoutDbContext.Profiles
                      .Where(p => p.FirstName.Contains(searchQuery) || p.LastName.Contains(searchQuery))
                      .ToList();
            

            //  (from p in linkedoutDbContext.Profiles
            //where searchTerms.Any(t => p.FirstName.StartsWith(t))
            //select p).ToList();

            return View(profiles);
            







        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Projects()
        {
            List<Project> projectlist = linkedoutDbContext.Projects.ToList();
            return View(projectlist);
        }
    }
}