using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Profile
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Vänligen fyll i förnamn")]
    [StringLength(100,ErrorMessage = "Ditt förnamn får vara max 100 karaktärer")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Vänligen fyll i efternamn")]
    [StringLength(100, ErrorMessage = "Ditt efternamn får vara max 100 karaktärer")]
    public string? LastName { get; set; }

    public int? Visitors { get; set; }
    [Required(ErrorMessage = "Vänligen fyll i epost")]
    [StringLength(100, ErrorMessage = "Din epost får vara max 100 karaktärer")]
    public string? Email { get; set; }

    public string? PicUrl { get; set; }

    public bool Private { get; set; }
    
    public string? UserName { get; set; }

    [ForeignKey(nameof(UserName))]
    public virtual User? user { get; set; }
    public virtual ICollection<Message> Messages { get; } = new List<Message>();

    public virtual ICollection<ProfileHasEducation> ProfileHasEducations { get; } = new List<ProfileHasEducation>();
    public virtual ICollection<ProfileinProject> ProfileinProjects { get; } = new List<ProfileinProject>();

    public virtual ICollection<ProfileHasExperience> ProfileHasExperiences { get; } = new List<ProfileHasExperience>();


    public virtual ICollection<Project> ProjectsNavigation { get; } = new List<Project>();


    public virtual ICollection<ProfileHasCompetence> ProfileHasCompetences { get; } = new List<ProfileHasCompetence>();

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
