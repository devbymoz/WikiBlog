using Microsoft.EntityFrameworkCore;

namespace WikiBlog.Models
{
    public class User
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public List<Article>? Articles { get; set; }
        public List<Comment>? Comments { get; set; }

        public string? AppUserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public AppUser? AppUser { get; set; }
    }
}
