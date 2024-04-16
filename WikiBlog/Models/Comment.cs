using System.ComponentModel.DataAnnotations;

namespace WikiBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [MaxLength(100)]
        public string Content { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
