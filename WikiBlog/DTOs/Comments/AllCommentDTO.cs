using WikiBlog.Models;

namespace WikiBlog.DTOs.Comments
{
    public class AllCommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
