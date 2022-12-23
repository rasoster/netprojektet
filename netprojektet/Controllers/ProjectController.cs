using Microsoft.AspNetCore.Mvc;
using netprojektet.Models.DataLayer;
using netprojektet.Models.ViewModels;
using System.Linq.Expressions;

namespace netprojektet.Controllers
{
    public class ProjectController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public ProjectController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }


        public IActionResult Project()
        {

            var model = new ProfileProjectViewModel();

            model.project = linkedoutDbContext.Projects.ToList();
            model.profiles = linkedoutDbContext.Profiles.ToList();
            model.profileInProject = linkedoutDbContext.ProfileinProjects.ToList();
            return View(model);

        }

        public IActionResult GåMed(int projectID)
        {
            ProfileinProject profileinProject = new ProfileinProject();
            profileinProject.Project = linkedoutDbContext.Projects.Find(projectID);
            
            profileinProject.Profile = (from p in linkedoutDbContext.Profiles
                                        where p.UserName == User.Identity.Name         
                                        select p).FirstOrDefault(); 

            profileinProject.Projectid = projectID;
            profileinProject.Profileid = profileinProject.Profile.Id;
            linkedoutDbContext.ProfileinProjects.Add(profileinProject);
            linkedoutDbContext.SaveChanges(); 
            

            return RedirectToAction("Project");
        }

        public IActionResult GåUr(int project)
        {
            int profileID = (from p in linkedoutDbContext.Profiles
                             where p.UserName == User.Identity.Name
                             select p.Id).FirstOrDefault();


            ProfileinProject profileinP = (from p in linkedoutDbContext.ProfileinProjects
                                           where p.Profileid == profileID && p.Projectid == project
                                           select p).FirstOrDefault();

            linkedoutDbContext.ProfileinProjects.Remove(profileinP);
            linkedoutDbContext.SaveChanges();

            return RedirectToAction("Project");
        }
        [HttpGet]
        public IActionResult addProject()
        {
            return View(new Project());
        }
        [HttpPost]
        public IActionResult AddProject(Project newProject)
        {
            Profile creator = linkedoutDbContext.Profiles.Where(e => e.UserName == User.Identity.Name).First();

            newProject.Creator = creator;
            newProject.CreatorId = creator.Id;
            
            linkedoutDbContext.Add(newProject);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Project");


        }
        [HttpGet]
        public IActionResult UpdateProject(int projectID)
        {
            Project projektToUpdate = linkedoutDbContext.Projects.Find(projectID);

            return View(projektToUpdate);
        }
        [HttpPost]
        public IActionResult UpdateProject(Project updatedProject)
        {
            linkedoutDbContext.Update(updatedProject);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Project");
        }
    }
}
