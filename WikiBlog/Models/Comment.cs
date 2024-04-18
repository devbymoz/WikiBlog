using Microsoft.EntityFrameworkCore;
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
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Article Article { get; set; }

        public int UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; }
    }
}
