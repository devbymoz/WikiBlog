using WikiBlog.DTOs.Comments;
using WikiBlog.Enums;
using WikiBlog.Models;

namespace WikiBlog.DTOs.Articles
{
    public class GetSingleArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Priotity Priotity { get; set; }
        public int AuthorArticleId { get; set; }
        public String AuthorArticleName { get; set; }
        public Theme Theme { get; set; }
        public List<AllCommentDTO>? CommentsDTO { get; set; }
    }
}


//Récupérer un article avec sa liste de commentaires et les infos de l’auteur de l’article, et pour chaque commentaire l’auteur du commentaire (email ou first name + id)