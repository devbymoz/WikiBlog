using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Models;

namespace WikiBlog.Interfaces.IRepositories
{
    public interface ICommentRepository
    {
        public Task<bool> CreateComment(CreateCommentDTO commentDTO);
        public Task<User?> GetAllCommentByUser(int idUser);
        public Task<Article?> GetAllCommentByArticle(int idArticle);
        public Task<Comment?> GetCommentbyId(int id);
        public Task<bool?> UpdateComment(UpdateCommentDTO paramCommentDTO);
        public Task<bool?> DeleteComment(int id);
    }
}


