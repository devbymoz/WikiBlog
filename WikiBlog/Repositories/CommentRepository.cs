using Microsoft.EntityFrameworkCore;
using WikiBlog.Config;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;

namespace WikiBlog.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private DbContextWikiBlog dbContextWikiBlog;

        public CommentRepository(DbContextWikiBlog dbContextWikiBlog)
        {
            this.dbContextWikiBlog = dbContextWikiBlog;
        }

        public async Task<bool> CreateComment(CreateCommentDTO commentDTO)
        {
            var comment = new Comment
            {
                Content = commentDTO.Content,
                CreationDate = DateTime.Now,
                Article = commentDTO.Article,
                User = commentDTO.User
            };

            try
            {
                await dbContextWikiBlog.Comments.AddAsync(comment);
                await dbContextWikiBlog.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User?> GetAllCommentByUser(int idUser)
        {
            try
            {
                User? userWithComments = await dbContextWikiBlog.Users
                    .Where(u => u.Id == idUser)
                    .Include(u => u.Comments)
                    .FirstOrDefaultAsync();

                return userWithComments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Article?> GetAllCommentByArticle(int idArticle)
        {
            try
            {
                Article? articleWithComments = await dbContextWikiBlog.Articles
                    .Where(a => a.Id == idArticle)
                    .Include(u => u.Comments)
                    .FirstOrDefaultAsync();

                return articleWithComments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Comment?> GetCommentbyId(int id)
        {
            try
            {
                Comment? comment = await dbContextWikiBlog.Comments.FirstOrDefaultAsync(c => c.Id == id);

                return comment;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool?> UpdateComment(UpdateCommentDTO paramCommentDTO)
        {
            Comment? comment = await dbContextWikiBlog.Comments.FindAsync(paramCommentDTO.Id);

            if (comment == null)
            {
                return false;
            }

            comment.Content = paramCommentDTO.Content;

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

        public async Task<bool?> DeleteComment(int id)
        {
            try
            {
                Comment? comment = await dbContextWikiBlog.Comments.FirstOrDefaultAsync(c => c.Id == id);

                if (comment == null)
                {
                    return false;
                }

                dbContextWikiBlog.Comments.Remove(comment);
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
