using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Themes;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;
using WikiBlog.Repositories;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ThemeController : ControllerBase
    {
        private IThemeRepository themeRepository;

        public ThemeController(IThemeRepository themeRepository)
        {
            this.themeRepository = themeRepository;
        }

        /// <summary>
        /// Création d'un nouveau thème
        /// </summary>
        /// <param name="themeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateTheme(CreateThemeDTO themeDTO)
        {
            bool checkingCreation = await themeRepository.CreateTheme(themeDTO);

            if (checkingCreation == false)
            {
                return StatusCode(500);
            }

            return Ok("Le thème a bien été créé");
        }

        /// <summary>
        /// Récupération de tous les thèmes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllThemes()
        {
            List<Theme>? themes = await themeRepository.GetAllThemes();

            if (themes == null || themes.Count == 0)
            {
                return NoContent();
            }

            return Ok(themes);
        }

        /// <summary>
        /// Récupération d'un thème grace à son identifiant
        /// </summary>
        /// <param name="id">int : identifiant du thème</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetThemeById(int id)
        {
            var theme = await themeRepository.GetThemeById(id);

            if (theme == null)
            {
                return NoContent();
            }

            return Ok(theme);
        }

        /// <summary>
        /// Modification d'un thème
        /// </summary>
        /// <param name="id">int : Identifiant du thème</param>
        /// <param name="themeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> UpdateTheme(int id, CreateThemeDTO themeDTO)
        {
            bool? checkingUpdate = await themeRepository.UpdateTheme(id, themeDTO);

            if (checkingUpdate == false)
            {
                return NoContent();
            }

            return Ok("Article modifié avec succes");
        }

        /// <summary>
        /// Suppression d'un thème
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteTheme(int id)
        {
            bool? checkingDelete = await themeRepository.DeleteTheme(id);

            if (checkingDelete == false)
            {
                return StatusCode(500);
            }

            return Ok("Thème supprimé");
        }



    }
}
