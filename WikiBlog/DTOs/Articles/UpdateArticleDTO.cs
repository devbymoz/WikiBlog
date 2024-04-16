using WikiBlog.Enums;
using WikiBlog.Models;

namespace WikiBlog.DTOs.Articles
{
    public class UpdateArticleDTO
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Priotity Priotity { get; set; }
        public Theme Theme { get; set; }
    }
}
