using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EducationViewModel
    {
        [Required(ErrorMessage = "Vänligen fyll i titel")]
        [StringLength(100, ErrorMessage = "namn får vara max 100 karaktärer")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vänligen fyll i beskrivning")]
        [StringLength(100, ErrorMessage = "beskrivning får vara max 200 karaktärer")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vänligen fyll i startdatum")]
        [DataType(DataType.Date)]
        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }


    }
}
