using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq.Expressions;
using DataAccessLayer;

namespace netprojektet.Controllers
{
    public class ProjectController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;

        public ProjectController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }

        //tar fram alla projekt
        public IActionResult Project()
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";

            var model = new ProfileProjectViewModel();

            model.profileInProject = linkedoutDbContext.ProfileinProjects.ToList();
            model.profiles = linkedoutDbContext.Profiles.ToList();
            if (User.Identity.IsAuthenticated)
            {
                model.project = linkedoutDbContext.Projects.ToList();
            }
            else
            {
                model.project = linkedoutDbContext.Projects.Where(p => p.Creator.Private == false).ToList();
            }
            return View(model);

        }
        //för att gå med i projekt
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
        //för att gå ur projekt
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
        //startar nytt projekt formuläret
        [HttpGet]
        public IActionResult addProject()
        {
            ViewBag.Meddelanden = "Du har " + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + " olästa meddelanden.";

            return View(new Project());
        }
        //skickar nytt projekt formuläret
        [HttpPost]
        public IActionResult AddProject(Project newProject)
        {
            ViewBag.Meddelanden = "Du har " + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + " olästa meddelanden.";

            Profile creator = linkedoutDbContext.Profiles.Where(e => e.UserName == User.Identity.Name).First();

            newProject.Creator = creator;
            newProject.CreatorId = creator.Id;
            
            linkedoutDbContext.Add(newProject);
            linkedoutDbContext.SaveChanges();

            GåMed(newProject.Id);

            return RedirectToAction("Project");


        }
        //startar uppdatera projekt formuläret
        [HttpGet]
        public IActionResult UpdateProject(int projectID)
        {
            Project projektToUpdate = linkedoutDbContext.Projects.Find(projectID);

            return View(projektToUpdate);
        }
        //skickar uppdatera projekt formuläret
        [HttpPost]
        public IActionResult UpdateProject(Project updatedProject)
        {   //samma som profil. Problem med foreign key så kopierar över information från viewModel till existerande objektet.
            Project theProject = linkedoutDbContext.Projects.FirstOrDefault(p => p.Id == updatedProject.Id);
            theProject.Title = updatedProject.Title;
            theProject.Description = updatedProject.Description;
            linkedoutDbContext.Update(theProject);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Project");
        }
        public IActionResult TaBort(int id)
        {
            List<ProfileinProject> profileinProjects = linkedoutDbContext.ProfileinProjects.Where(p => p.Projectid == id).ToList();
            foreach(ProfileinProject profile in profileinProjects)
            {
                linkedoutDbContext.Remove(profile);
            }

            Project theProject = linkedoutDbContext.Projects.Find(id);
            linkedoutDbContext.Remove(theProject);
            linkedoutDbContext.SaveChanges();
            return RedirectToAction("Project");
        }
    }
}
