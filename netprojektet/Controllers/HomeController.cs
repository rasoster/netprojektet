using Microsoft.AspNetCore.Mvc;
using netprojektet.Models;
using System.Diagnostics;
using netprojektet.Models.DataLayer;
using Microsoft.AspNetCore.Authorization;

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


            List<Profile> profileListFull = linkedoutDbContext.Profiles.ToList();
            List<Profile> profileListLimited = linkedoutDbContext.Profiles.Where(e => e.Private == false).ToList();

            if (User.Identity.IsAuthenticated)
            {
                return View(profileListFull);
            }
            else { 
            return View(profileListLimited);
            }
        }
        

        public IActionResult Project()
        {
            List<Project> projectList= linkedoutDbContext.Projects.ToList();
            return View(projectList);
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