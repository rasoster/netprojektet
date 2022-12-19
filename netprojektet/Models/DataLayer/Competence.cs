using System;
using System.Collections.Generic;

namespace netprojektet.Models.DataLayer;

public partial class Competence
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Profile> Profiles { get; } = new List<Profile>();
}
