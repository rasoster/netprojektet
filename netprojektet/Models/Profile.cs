namespace netprojektet.Models
{
    public class Profile
    {
        public int ProfileID { get; set; }

        public string PicUrl { get; set; }

        public string FirstName { get; set;}

        public string LastName { get; set;}
        public int Visitors { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool PrivateProfile { get; set; }

        public User User { get; set; }

    }
}
