using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Profile
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Visitors { get; set; }

    public string? Email { get; set; }

    public string? PicUrl { get; set; }

    public bool? Private { get; set; }
    
    public string UserName { get; set; }

    [ForeignKey(nameof(UserName))]
    public virtual User user { get; set; }
    public virtual ICollection<Message> Messages { get; } = new List<Message>();

    public virtual ICollection<ProfileHasEducation> ProfileHasEducations { get; } = new List<ProfileHasEducation>();
    public virtual ICollection<ProfileinProject> ProfileinProjects { get; } = new List<ProfileinProject>();

    public virtual ICollection<ProfileHasExperience> ProfileHasExperiences { get; } = new List<ProfileHasExperience>();

    public virtual ICollection<Project> ProjectsNavigation { get; } = new List<Project>();


    public virtual ICollection<Competence> Competences { get; } = new List<Competence>();

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
