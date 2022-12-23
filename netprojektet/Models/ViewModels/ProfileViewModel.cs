using netprojektet.Models.DataLayer;
namespace netprojektet.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Profile profile { get; set; }
        public List<ProfileHasEducation> profileHasEducation { get; set; }
        public List<ProfileHasExperience> profileHasExperience { get; set; }
        public List<ProfileinProject> profileinProject { get; set; }

        public List<Project> project { get; set; }
        public List<Project> Education { get; set; }
        public List<Project> Experience { get; set; }


    }
}
