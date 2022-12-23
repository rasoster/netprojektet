using System;
using System.Collections.Generic;

namespace Models;

public partial class Project
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public int CreatorId { get; set; }

    public virtual Profile Creator { get; set; } = null!;

    public virtual ICollection<Profile> profiles{ get; } = new List<Profile>();
    public virtual ICollection<ProfileinProject> ProfileinProjects { get; } = new List<ProfileinProject>();

}
