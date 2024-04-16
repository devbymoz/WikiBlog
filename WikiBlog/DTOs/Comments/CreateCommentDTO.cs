using WikiBlog.Models;

namespace WikiBlog.DTOs.Comments
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }
        public Article Article { get; set; }
        public User User { get; set; }
    }
}
