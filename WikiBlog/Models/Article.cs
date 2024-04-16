﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WikiBlog.Enums;

namespace WikiBlog.Models
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Priotity Priotity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int ThemeId { get; set; }
        public Theme Theme { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}

