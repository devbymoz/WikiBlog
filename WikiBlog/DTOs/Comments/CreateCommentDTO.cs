using WikiBlog.Models;

namespace WikiBlog.DTOs.Comments
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }
        public int ArticleId { get; set; }
    }
}
