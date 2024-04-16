using WikiBlog.DTOs.Articles;
using WikiBlog.Models;

namespace WikiBlog.Interfaces.IRepositories
{
    public interface IArticleRepository
    {
        public Task<bool> CreateArticle(CreateArticleDTO articleDTO);
        public Task<List<Article>?> GetAllArticles();
        public Task<Article?> GetArticleById(int id);
        public Task<bool?> UpdateArticle(UpdateArticleDTO paramArticleDTO);
        public Task<bool?> DeleteArticle(int id);
    }
}
