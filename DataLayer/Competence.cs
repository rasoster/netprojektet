using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Competence
{
    public int Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }


    public virtual ICollection<ProfileHasCompetence> ProfileHasCompetences { get; } = new List<ProfileHasCompetence>();

}
