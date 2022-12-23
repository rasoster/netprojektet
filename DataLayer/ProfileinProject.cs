namespace Models
{
    public class ProfileinProject
    {
        public int Profileid { get; set; }

        public int Projectid { get; set; }

        public virtual Project Project { get; set; } = null!;

        public virtual Profile Profile { get; set; } = null!;
    }
}
