using System.ComponentModel.DataAnnotations.Schema;

namespace netprojektet.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int ProfileID { get; set; }

        [ForeignKey (nameof(ProfileID))]
        public Profile profile { get; set; }
    }
}
