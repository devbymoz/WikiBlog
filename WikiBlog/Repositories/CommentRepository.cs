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

        public async Task<bool> CreateComment(CreateCommentDTO commentDTO, int userId)
        {
            var comment = new Comment
            {
                Content = commentDTO.Content,
                CreationDate = DateTime.Now,
                ArticleId = commentDTO.ArticleId,
                UserId = userId
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

        public async Task<List<AllCommentDTO>?> GetAllComment()
        {
            try
            {
                List<Comment>? comments = await dbContextWikiBlog.Comments
                    .Include(u => u.User.AppUser)
                    .ToListAsync();

                List<AllCommentDTO> commentsDTO = new List<AllCommentDTO>();

                if (comments != null)
                {
                    foreach (var comment in comments)
                    {
                        commentsDTO.Add(new AllCommentDTO
                        {
                            Id = comment.Id,
                            Content = comment.Content,
                            UserId= comment.UserId,
                            UserName = comment.User.AppUser.UserName
                        });
                    }
                }

                return commentsDTO;
            }
            catch (Exception)
            {
                throw;
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

        public async Task<bool?> UpdateComment(int id, UpdateCommentDTO paramCommentDTO, int userId, bool isAdmin)
        {
            Comment? comment = await dbContextWikiBlog.Comments.FindAsync(id);

            if (comment == null)
            {
                return false;
            }

            if (comment.UserId == userId || isAdmin == true)
            {
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
            else
            {
                return false;
            }


        }

        public async Task<bool?> DeleteComment(int id, int userId, bool isAdmin)
        {
            Comment? comment = await dbContextWikiBlog.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return false;
            }

            if (comment.UserId == userId || isAdmin == true)
            {
                try
                {
                    dbContextWikiBlog.Comments.Remove(comment);
                    await dbContextWikiBlog.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
