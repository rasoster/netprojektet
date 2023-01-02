using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv ett Lösenord.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vänligen skriv ett Lösenord.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vänligen Bekräfta Lösenordet.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Bekrafta losenordet")]
        public string ConfirmPassword { get; set;}
    }
}
