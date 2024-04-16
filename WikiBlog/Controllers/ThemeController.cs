using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.Interfaces.IRepositories;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private IThemeRepository themeRepository;

        public ThemeController(IThemeRepository themeRepository)
        {
            this.themeRepository = themeRepository;
        }


    }
}
