using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;

namespace Models
{
    public class ProfileViewModel
    {
        public Profile profile { get; set; }
        public List<ProfileHasEducation> profileHasEducation { get; set; }
        public List<ProfileHasExperience> profileHasExperience { get; set; }
        public List<ProfileinProject> profileinProject { get; set; }

        public List<ProfileHasCompetence> profileHasCompetence { get; set; }

        public List<Profile> similarProject { get; set; } = new List<Profile>();
        public List<Profile> similarEducation { get; set; }

        public List<Profile> similarCompetence { get; set; }
        public List<Profile> similarExperience { get; set; }

        public List<Project> project { get; set; }

        public List<GitHubRepository> gitHubRepository { get; set; }

        [XmlIgnore]
        public IFormFile Image { get; set; }
      


    }
}
