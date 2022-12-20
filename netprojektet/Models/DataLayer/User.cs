using Microsoft.AspNetCore.Identity;
namespace netprojektet.Models.DataLayer

{
    public class User:IdentityUser
    {
        public virtual Profile profile { get; set; }
    }
}
