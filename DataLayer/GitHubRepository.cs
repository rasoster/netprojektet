

namespace Models
{
    public class GitHubRepository
    {
        public int id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }

        public string description { get; set; }

        public int watchers_count { get; set; }
    }
}
