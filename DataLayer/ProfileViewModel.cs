using Microsoft.AspNetCore.Http;
using Models;


namespace Models
{
    public class ProfileViewModel
    {
        public Profile profile { get; set; }
        public List<ProfileHasEducation> profileHasEducation { get; set; }
        public List<ProfileHasExperience> profileHasExperience { get; set; }
        public List<ProfileinProject> profileinProject { get; set; }

        public List<ProfileHasCompetence> profileHasCompetence { get; set; }

        public List<Project> project { get; set; }

        public IFormFile Image { get; set; }
      


    }
}
