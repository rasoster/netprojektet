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

        //[Authorize] lägger till så att man måste logga in innan homepagen syns
        public IActionResult Index()
        {
            List<Profile> profileList = linkedoutDbContext.Profiles.ToList();
            

            return View(profileList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}