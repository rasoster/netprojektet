using System;
using System.Collections.Generic;

namespace netprojektet.Models.DataLayer;

public partial class Project
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public int CreatorId { get; set; }

    public virtual Profile Creator { get; set; } = null!;

    public virtual ICollection<Profile> Profiles { get; } = new List<Profile>();
}
