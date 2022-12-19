using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace netprojektet.Models.ViewModels
    
{
    public class RegisterViewModel
    {
    [Required(ErrorMessage = "Vänligen skriv ett användarnamn.")]
    [StringLength(255)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Vänligen skriv ett Lösenord.")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Vänligen Bekräfta Lösenordet.")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Bekrafta losenordet")]
    public string ConfirmPassword { get; set; }
}
}

