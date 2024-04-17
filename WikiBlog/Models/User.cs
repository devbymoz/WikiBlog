namespace WikiBlog.Models
{
    public class User
    {
        public int Id { get; set; }

        public List<Article>? Articles { get; set; }
        public List<Comment>? Comments { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
