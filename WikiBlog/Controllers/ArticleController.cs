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
        [ProducesResponseType(200)]
        public async Task<ActionResult> CreateArticle(CreateArticleDTO articleDTO)
        {
            await articleRepository.CreateArticle(articleDTO);

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
            List<Article>? articles = await articleRepository.GetAllArticles();

            if (articles == null || articles.Count == 0)
            {
                return NoContent();
            }

            return Ok(articles);
        }

        /// <summary>
        /// Récupération d'un article grace à son identifiant
        /// </summary>
        /// <param name="id">int : identifiant de l'article</param>
        /// <returns></returns>
        [HttpGet("id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetArticleById(int id)
        {
            var article = await articleRepository.GetArticleById(id);

            if (article == null)
            {
                return NoContent();
            }

            return Ok(article);
        }

        /// <summary>
        /// Modification d'un article
        /// </summary>
        /// <param name="articleDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<ActionResult> UpdateArticle(UpdateArticleDTO articleDTO)
        {
            await articleRepository.UpdateArticle(articleDTO);

            return Ok("Article modifié avec succes");
        }

        /// <summary>
        /// Suppression d'un article
        /// </summary>
        /// <param name="id">int : identifiant de l'article</param>
        /// <returns></returns>
        [HttpDelete("id")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            await articleRepository.DeleteArticle(id);

            return Ok("Article supprimé");
        }


    }
}
