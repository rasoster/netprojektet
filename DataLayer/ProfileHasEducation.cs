using System;
using System.Collections.Generic;

namespace Models;

public partial class ProfileHasEducation
{
    public int Profileid { get; set; }

    public int Educationid { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public virtual Education Education { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}
