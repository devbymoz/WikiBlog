using WikiBlog.Enums;
using WikiBlog.Models;

namespace WikiBlog.DTOs.Articles
{
    public class UpdateArticleDTO
    {
        public int Id { get; }
        private DateTime updateDate;
        public string Title { get; set; }
        public string Content { get; set; }
        public Priotity Priotity { get; set; }
        public Theme Theme { get; set; }

        public UpdateArticleDTO(string title, string content, Priotity priority, Theme theme)
        {
            this.updateDate = DateTime.Now;
            this.Title = title;
            this.Content = content;
            this.Priotity = priority;
            this.Theme = theme;
        }
    }
}
