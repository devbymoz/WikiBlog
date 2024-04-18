using WikiBlog.DTOs.Articles;
using WikiBlog.Enums;
using WikiBlog.Models;

namespace WikiBlog.Interfaces.IRepositories
{
    public interface IArticleRepository
    {
        public Task<bool> CreateArticle(CreateArticleDTO articleDTO, int idUserConnect);
        public Task<List<AllArticleDTO>?> GetAllArticles();
        public Task<GetSingleArticleDTO?> GetArticleById(int id);
        public Task<bool?> UpdateArticle(int id, UpdateArticleDTO paramArticleDTO, int userId, bool isAdmin);
        public Task<bool?> DeleteArticle(int id, int userId, bool isAdmin);
        public Task<bool?> ChangePriorityArticle(int id, Priotity priotity);

    }
}
