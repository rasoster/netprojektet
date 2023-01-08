

namespace Models;

public partial class Education
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProfileHasEducation> ProfileHasEducations { get; } = new List<ProfileHasEducation>();
}
