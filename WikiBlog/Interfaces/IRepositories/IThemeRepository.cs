using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.DTOs.Themes;
using WikiBlog.Models;

namespace WikiBlog.Interfaces.IRepositories
{
    public interface IThemeRepository
    {
        public Task<bool> CreateTheme(CreateThemeDTO themeDTO);
        public Task<List<Theme>?> GetAllThemes();
        public Task<Theme?> GetThemeById(int id);
        public Task<bool?> UpdateTheme(CreateThemeDTO themeDTO);
        public Task<bool?> DeleteTheme(int id);
    }
}
