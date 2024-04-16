using Microsoft.EntityFrameworkCore;
using WikiBlog.Config;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.DTOs.Themes;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;

namespace WikiBlog.Repositories
{
    public class ThemeRepository : IThemeRepository
    {
        private DbContextWikiBlog dbContextWikiBlog;

        public ThemeRepository(DbContextWikiBlog dbContextWikiBlog)
        {
            this.dbContextWikiBlog = dbContextWikiBlog;
        }

        public async Task<bool> CreateTheme(CreateThemeDTO themeDTO)
        {
            var theme = new Theme
            {
                Name = themeDTO.Name,
            };

            try
            {
                await dbContextWikiBlog.Themes.AddAsync(theme);
                await dbContextWikiBlog.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<List<Theme>?> GetAllThemes()
        {
            try
            {
                List<Theme> themes = await dbContextWikiBlog.Themes.ToListAsync();

                return themes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Theme?> GetThemeById(int id)
        {
            try
            {
                Theme? theme = await dbContextWikiBlog.Themes.FirstOrDefaultAsync(a => a.Id == id);

                return theme;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool?> UpdateTheme(CreateThemeDTO themeDTO)
        {
            Theme? theme = await dbContextWikiBlog.Themes.FindAsync(themeDTO.Id);

            if (theme == null)
            {
                return false;
            }

            theme.Name = themeDTO.Name;

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

        public async Task<bool?> DeleteTheme(int id)
        {
            try
            {
                Theme? theme = await dbContextWikiBlog.Themes.FirstOrDefaultAsync(t => t.Id == id);

                if (theme == null)
                {
                    return false;
                }

                dbContextWikiBlog.Themes.Remove(theme);
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
