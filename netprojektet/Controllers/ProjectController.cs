using Microsoft.AspNetCore.Mvc;
using netprojektet.Models.DataLayer;
using netprojektet.Models.ViewModels;

namespace netprojektet.Controllers
{
    public class ProjectController : Controller
    {
        private LinkedoutDbContext _linkedoutDbContext;

        public ProjectController(LinkedoutDbContext linkedoutDbContext) 
        {
        _linkedoutDbContext= linkedoutDbContext;
        }


        public IActionResult Project()
        {
           
            
            return View();
          
        }
    }
}
