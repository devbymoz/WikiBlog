using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Models;

namespace WikiBlog.Interfaces.IRepositories
{
    public interface ICommentRepository
    {
        public Task<bool> CreateComment(CreateCommentDTO commentDTO, int userID);
        public Task<List<AllCommentDTO>?> GetAllComment();
        public Task<User?> GetAllCommentByUser(int idUser);
        public Task<Article?> GetAllCommentByArticle(int idArticle);
        public Task<Comment?> GetCommentbyId(int id);
        public Task<bool?> UpdateComment(int id, UpdateCommentDTO paramCommentDTO, int userId);
        public Task<bool?> DeleteComment(int id, int userId);
    }
}


