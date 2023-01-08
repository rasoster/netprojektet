using System.ComponentModel.DataAnnotations;


namespace Models;

public partial class Project
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Vänligen skriv in en beskrivning")]
    [StringLength(200,ErrorMessage = "Beskrivningen får vara max 200 tecken")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Vänligen skriv in en titel")]
    [StringLength(200, ErrorMessage = "Beskrivningen får vara max 200 tecken")]
    public string Title { get; set; }
    public int CreatorId { get; set; }
    public virtual Profile? Creator { get; set; }

    public virtual ICollection<Profile> profiles{ get; } = new List<Profile>();
    public virtual ICollection<ProfileinProject> ProfileinProjects { get; } = new List<ProfileinProject>();

}
