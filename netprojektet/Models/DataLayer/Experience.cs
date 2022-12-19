using System;
using System.Collections.Generic;

namespace netprojektet.Models.DataLayer;

public partial class Experience
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProfileHasExperience> ProfileHasExperiences { get; } = new List<ProfileHasExperience>();
}
