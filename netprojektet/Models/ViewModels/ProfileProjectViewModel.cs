using netprojektet.Models.DataLayer;
namespace netprojektet.Models.ViewModels

{
    public class ProfileProjectViewModel
    {
        public List<Profile> profiles { get; set; }
        public List<ProfileinProject> profileInProject { get; set; }
        public List<Project> project { get; set; }
        public Project senasteProject { get; set; }

    }
}
