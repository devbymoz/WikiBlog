using WikiBlog.Config;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;

namespace WikiBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbContextWikiBlog dbContextWikiBlog;

        public UserRepository(DbContextWikiBlog dbContextWikiBlog)
        {
            this.dbContextWikiBlog = dbContextWikiBlog;
        }

        /// <summary>
        /// Permet de récupérer l'id du User standard connecté 
        /// </summary>
        /// <param name="uuidAppuUser"></param>
        /// <returns>int : L'identifiant du user standard</returns>
        public int GetUserId(string uuidAppuUser)
        {
            User? user = dbContextWikiBlog.Users
                .Where(u => u.AppUserId == uuidAppuUser)
                .FirstOrDefault();

            if (user == null)
            {
                return 0;
            }

            if (user.AppUserId != uuidAppuUser)
            {
                return 0;
            }

            return user.Id;
        }

    }
}
