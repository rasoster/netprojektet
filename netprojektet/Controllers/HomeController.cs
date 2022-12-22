using Microsoft.AspNetCore.Mvc;
using netprojektet.Models;
using System.Diagnostics;
using netprojektet.Models.DataLayer;
using Microsoft.AspNetCore.Authorization;
using netprojektet.Models.ViewModels;

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