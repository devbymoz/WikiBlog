using WikiBlog.DTOs.Articles;
using WikiBlog.Models;

namespace WikiBlog.Interfaces.IRepositories
{
    public interface IArticleRepository
    {
        public Task<bool> CreateArticle(CreateArticleDTO articleDTO);
        public Task<List<AllArticleDTO>?> GetAllArticles();
        public Task<GetSingleArticleDTO?> GetArticleById(int id);
        public Task<bool?> UpdateArticle(int id, UpdateArticleDTO paramArticleDTO);
        public Task<bool?> DeleteArticle(int id);
    }
}
