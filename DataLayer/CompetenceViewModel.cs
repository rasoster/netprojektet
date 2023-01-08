
using System.ComponentModel.DataAnnotations;


namespace Models
{
    public class CompetenceViewModel
    {
        [Required(ErrorMessage = "Vänligen fyll i titel")]
        [StringLength(100, ErrorMessage = "namn får vara max 100 karaktärer")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vänligen fyll i beskrivning")]
        [StringLength(100, ErrorMessage = "beskrivning får vara max 200 karaktärer")]
        public string Description { get; set; }


      


    }
}
