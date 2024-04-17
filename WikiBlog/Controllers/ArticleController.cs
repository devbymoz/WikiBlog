using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.DTOs.Articles;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        /// <summary>
        /// Création d'un nouvel article
        /// </summary>
        /// <param name="articleDTO">Article</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> CreateArticle(CreateArticleDTO articleDTO)
        {
            bool checkingCreation = await articleRepository.CreateArticle(articleDTO);

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
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]

        public async Task<ActionResult> UpdateArticle(int id, UpdateArticleDTO articleDTO)
        {
            bool? checkingUpdate = await articleRepository.UpdateArticle(id, articleDTO);

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
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            bool? checkingDelete = await articleRepository.DeleteArticle(id);

            if (checkingDelete == false)
            {
                return StatusCode(500);
            }

            return Ok("Article supprimé");
        }


    }
}
