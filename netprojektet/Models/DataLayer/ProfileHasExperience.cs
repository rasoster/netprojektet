using System;
using System.Collections.Generic;

namespace netprojektet.Models.DataLayer;

public partial class ProfileHasExperience
{
    public int Profileid { get; set; }

    public int Experienceid { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public virtual Experience Experience { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}
