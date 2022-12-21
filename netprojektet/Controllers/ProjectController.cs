using Microsoft.AspNetCore.Mvc;
using netprojektet.Models.DataLayer;
using netprojektet.Models.ViewModels;
using System.Linq.Expressions;

namespace netprojektet.Controllers
{
    public class ProjectController : Controller
    {
        private LinkedoutDbContext _linkedoutDbContext;

        public ProjectController(LinkedoutDbContext linkedoutDbContext)
        {
            _linkedoutDbContext = linkedoutDbContext;
        }


        public IActionResult Project()
        {

            var model = new ProfileProjectViewModel();

            model.project = _linkedoutDbContext.Projects.ToList();
            model.profiles = _linkedoutDbContext.Profiles.ToList();
            model.profileInProject = _linkedoutDbContext.ProfileinProjects.ToList();
            return View(model);

        }

        public IActionResult GåMed(int projectID, int UserID) 
        {
            ProfileinProject profileinProject = new ProfileinProject();
            profileinProject.Projectid = projectID;
            profileinProject.Profileid = UserID;
            _linkedoutDbContext.Add(profileinProject);
            _linkedoutDbContext.SaveChanges(); 
            

            return RedirectToAction("Project");
        }

        public IActionResult GåUr()
        {
            return View();
        }
    }
}
