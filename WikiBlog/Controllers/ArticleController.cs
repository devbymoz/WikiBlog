using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.Const;
using WikiBlog.DTOs.Articles;
using WikiBlog.Enums;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;
using WikiBlog.Services;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private IArticleRepository articleRepository;
        private UserManager<AppUser> userManager;
        private IUserRepository userRepository;
        public ArticleController(IArticleRepository articleRepository, UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            this.articleRepository = articleRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Création d'un nouvel article
        /// </summary>
        /// <param name="articleDTO">Article</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> CreateArticle(CreateArticleDTO articleDTO)
        {
            string? userConnectID = userManager.GetUserId(User);

            bool checkingCreation = await articleRepository.CreateArticle(articleDTO, userRepository.GetUserId(userConnectID));

            if (checkingCreation == false)
            {
                return StatusCode(500);
            }

            return Ok("Votre article a bien été créé");
        }

        /// <summary>
        /// Récupération de tous les articles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllArticles()
        {
            List<AllArticleDTO>? articlesDTO = await articleRepository.GetAllArticles();

            if (articlesDTO == null || articlesDTO.Count == 0)
            {
                return NoContent();
            }

            return Ok(articlesDTO);
        }

        /// <summary>
        /// Récupération d'un article grace à son identifiant
        /// </summary>
        /// <param name="id">int : identifiant de l'article</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetArticleById(int id)
        {
            var articleDto = await articleRepository.GetArticleById(id);

            if (articleDto == null)
            {
                return NoContent();
            }

            return Ok(articleDto);
        }

        /// <summary>
        /// Modification d'un article
        /// </summary>
        /// <param name="id">int : Identifiant de l'article</param>
        /// <param name="articleDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> UpdateArticle(int id, UpdateArticleDTO articleDTO)
        {
            string? userConnectID = userManager.GetUserId(User);

            var appUser = await userManager.GetUserAsync(User);
            bool isAdmin = await userManager.IsInRoleAsync(appUser, "ADMIN");

            bool? checkingUpdate = await articleRepository.UpdateArticle(id, articleDTO, userRepository.GetUserId(userConnectID), isAdmin);

            if (checkingUpdate == false)
            {
                return NoContent();
            }

            return Ok("Article modifié avec succes");
        }

        /// <summary>
        /// Suppression d'un article
        /// </summary>
        /// <param name="id">int : identifiant de l'article</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            string? userConnectID = userManager.GetUserId(User);

            var appUser = await userManager.GetUserAsync(User);
            bool isAdmin = await userManager.IsInRoleAsync(appUser, "ADMIN");

            bool? checkingDelete = await articleRepository.DeleteArticle(id, userRepository.GetUserId(userConnectID), isAdmin);

            if (checkingDelete == false)
            {
                return StatusCode(500);
            }

            return Ok("Article supprimé");
        }


        /// <summary>
        /// Modification de la priorité d'un article
        /// </summary>
        /// <param name="id">int : Identifiant de l'article</param>
        /// <param name="priotity">0 : High, 1 : Normal</param>
        /// <returns></returns>
        [HttpPut("{id}, {priotity}")]
        [Authorize(Roles = $"{Roles.ADMIN}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> ChangePriority(int id, Priotity priotity)
        {
            bool? changePriority = await articleRepository.ChangePriorityArticle(id, priotity);

            if (changePriority == false)
            {
                return NoContent();
            }

            return Ok("La priorité de l'article a bien été modifiée");
        }





    }
}
