using WikiBlog.Enums;
using WikiBlog.Models;

namespace WikiBlog.DTOs.Articles
{
    public class CreateArticleDTO
    {
        private DateTime creationDate = DateTime.Now;
        public string Title {  get; set; }
        public string Content { get; set; }
        public Priotity Priotity { get; set; }
        public User User { get; set; }
        public Theme Theme { get; set; }
    }
}
