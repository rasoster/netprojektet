using Models;

namespace Models

{
    public class ProfileProjectViewModel
    {
        public List<Profile> profiles { get; set; }
        public List<ProfileinProject> profileInProject { get; set; }
        public List<Project> project { get; set; }

        public List<ProfileHasEducation> educations { get; set; }
        public List<ProfileHasExperience> experiences { get; set; }

        public List<ProfileHasCompetence> competences { get; set; }

        public Project senasteProject { get; set; }

    }
}
