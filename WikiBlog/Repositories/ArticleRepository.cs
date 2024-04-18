using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using System.Collections.Generic;
using WikiBlog.Config;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Enums;
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

        public async Task<bool> CreateArticle(CreateArticleDTO articleDTO, int idUserConnect)
        {
            var article = new Article
            {
                Title = articleDTO.Title,
                Content = articleDTO.Content,
                CreationDate = DateTime.Now,
                UserId = idUserConnect,
                ThemeId = articleDTO.ThemeId,
            };

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

        public async Task<List<AllArticleDTO>?> GetAllArticles()
        {
            try
            {
                List<Article> articles = await dbContextWikiBlog.Articles
                    .Include(t => t.Theme)
                    .OrderByDescending(t => t.Priotity == Priotity.High)
                    .ToListAsync();

                List<AllArticleDTO> articlesDTO = new List<AllArticleDTO>();

                foreach (var article in articles)
                {
                    articlesDTO.Add(new AllArticleDTO
                    {
                        Id = article.Id,
                        CreationDate = article.CreationDate,
                        Title = article.Title,
                        Content = article.Content,
                        Priotity = article.Priotity,
                        NameTheme = article.Theme.Name,
                    }); 
                }

                return articlesDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<GetSingleArticleDTO?> GetArticleById(int id)
        {
            try
            {
                Article? article = await dbContextWikiBlog.Articles
                    .Include(u => u.User)
                    .Include(c => c.Comments)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (article == null)
                {
                    return null;
                }

                List<AllCommentDTO>? articleComments = new List<AllCommentDTO>();

                if (article.Comments != null)
                {
                    foreach (var articleComment in article.Comments)
                    {
                        articleComments.Add(new AllCommentDTO
                        {
                            Id = articleComment.Id,
                            Content = articleComment.Content
                        });
                    }
                }

                GetSingleArticleDTO articleDTO = new GetSingleArticleDTO 
                { 
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    Priotity = article.Priotity,
                    AuthorArticleId = article.UserId,
                    AuthorArticleName = article.User.AppUser.UserName,
                    Theme = article.Theme,
                    CommentsDTO = articleComments
                };


                return articleDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool?> UpdateArticle(int id, UpdateArticleDTO paramArticleDTO, int userId, bool isAdmin)
        {
            Article? article = await dbContextWikiBlog.Articles.FindAsync(id);

            // Comment faire pour retourner des erreurs plus explicites
            if (article == null)
            {
                return false;
            }

            if (article.UserId !=  userId || isAdmin == false)
            {
                return false;
            }

            article.UpdateDate = DateTime.Now;
            article.Title = paramArticleDTO.Title;
            article.Content = paramArticleDTO.Content;
            article.ThemeId = paramArticleDTO.ThemeId;

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

        public async Task<bool?> DeleteArticle(int id, int userId, bool isAdmin)
        {
            try
            {
                Article? article = await dbContextWikiBlog.Articles
                    .Include(c => c.Comments)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (article == null)
                {
                    return false;
                }

                if (article.UserId != userId || isAdmin == false)
                {
                    return false;
                }

                List<Comment>? comments = article.Comments;

                if (comments != null)
                {
                    foreach (var comment in comments)
                    {
                        dbContextWikiBlog.Comments.Remove(comment);
                    }
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

        public async Task<bool?> ChangePriorityArticle(int id, Priotity priotity)
        {
            Article? article = await dbContextWikiBlog.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) 
            {
                return false;
            }

            article.Priotity = priotity;

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




    }
}
