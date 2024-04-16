using Microsoft.EntityFrameworkCore;
using WikiBlog.Config;
using WikiBlog.DTOs.Articles;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;

namespace WikiBlog.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private DbContextWikiBlog dbContextWikiBlog;

        public ArticleRepository(DbContextWikiBlog dbContextWikiBlog)
        {
            this.dbContextWikiBlog = dbContextWikiBlog;
        }

        public async Task<bool> CreateArticle(Article article)
        {
            try
            {
                await dbContextWikiBlog.Articles.AddAsync(article);
                await dbContextWikiBlog.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Article>?> GetAllArticles()
        {
            try
            {
                List<Article> articles = await dbContextWikiBlog.Articles.ToListAsync();

                return articles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Article?> GetArticleById(int id)
        {
            try
            {
                Article? article = await dbContextWikiBlog.Articles.FirstOrDefaultAsync(a => a.Id == id);

                return article;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool?> UpdateArticle(UpdateArticleDTO paramArticleDTO)
        {
            Article? article = await dbContextWikiBlog.Articles.FindAsync(paramArticleDTO.Id);

            if (article == null)
            {
                return false;
            }

            article.Title = paramArticleDTO.Title;
            article.Content = paramArticleDTO.Content;
            article.Theme = paramArticleDTO.Theme;
            article.Priotity = paramArticleDTO.Priotity;

            try
            {
                await dbContextWikiBlog.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool?> DeleteArticle(int id)
        {
            try
            {
                Article? article = await dbContextWikiBlog.Articles.FirstOrDefaultAsync(a => a.Id == id);

                if (article == null)
                {
                    return false;
                }

                dbContextWikiBlog.Articles.Remove(article);
                await dbContextWikiBlog.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
