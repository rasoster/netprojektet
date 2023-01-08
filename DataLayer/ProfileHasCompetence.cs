

namespace Models;

public partial class ProfileHasCompetence
{
    public int Profileid { get; set; }

    public int Competenceid { get; set; }

    public virtual Competence Competence { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}
