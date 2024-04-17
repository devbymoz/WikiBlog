using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.Const;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;
using WikiBlog.Repositories;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class CommentController : ControllerBase
    {
        private ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        /// <summary>
        /// Création d'un nouveau commentaire
        /// </summary>
        /// <param name="commentDTO">Commentaire</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> CreateComment(CreateCommentDTO commentDTO)
        {
            bool checkingCreation = await commentRepository.CreateComment(commentDTO);

            if (checkingCreation == false)
            {
                return StatusCode(500);
            }

            return Ok("Votre commentaire a bien été créé");
        }

        /// <summary>
        /// Récupération de la liste des commentaire avec leur auteur
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllComments()
        {
            List<AllCommentDTO>? commentsDTO = await commentRepository.GetAllComment();

            if (commentsDTO == null || commentsDTO.Count == 0)
            {
                return NoContent();
            }

            return Ok(commentsDTO);
        }

        /// <summary>
        /// Récupération de tous les commentaires d'un utilisateur
        /// </summary>
        /// <param name="id">int : Identifiant utilisateur</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllCommentByUser(int id)
        {
            var commentByUser = await commentRepository.GetAllCommentByUser(id);

            if (commentByUser == null)
            {
                return NoContent();
            }

            return Ok(commentByUser);
        }

        /// <summary>
        /// Récupération de tous les commentaires d'un article
        /// </summary>
        /// <param name="id">int : Identifiant de l'article</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllCommentByArticle(int id)
        {
            var commentByArticle = await commentRepository.GetAllCommentByArticle(id);

            if (commentByArticle == null)
            {
                return NoContent();
            }

            return Ok(commentByArticle);
        }

        /// <summary>
        /// Récupération d'un commentaire grace à son identifiant
        /// </summary>
        /// <param name="id">int : identifiant du commentaire</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetCommentById(int id)
        {
            var comment = await commentRepository.GetCommentbyId(id);

            if (comment == null)
            {
                return NoContent();
            }

            return Ok(comment);
        }

        /// <summary>
        /// Modification d'un commentaire
        /// </summary>
        /// <param name="id">int : Identifiant du commentaire</param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateComment(int id, UpdateCommentDTO commentDTO)
        {
            bool? checkingUpdate = await commentRepository.UpdateComment(id, commentDTO);

            if (checkingUpdate == false)
            {
                return StatusCode(500);
            } 

            return Ok("Commemtaire modifié avec succes");
        }

        /// <summary>
        /// Suppression d'un commentaire
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteComment(int id)
        {
            bool? checkingDelete = await commentRepository.DeleteComment(id);

            if (checkingDelete == false)
            {
                return StatusCode(500);
            }

            return Ok("Commentaire supprimé");
        }




    }
}
