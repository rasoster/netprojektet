using System.ComponentModel.DataAnnotations;

namespace Models

{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv ett användarnamn.")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vänligen skriv ett Lösenord.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
