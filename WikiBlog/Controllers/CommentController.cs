using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Repositories;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        [ProducesResponseType(200)]
        public async Task<ActionResult> CreateComment(CreateCommentDTO commentDTO)
        {
            await commentRepository.CreateComment(commentDTO);

            return Ok("Votre commentaire a bien été créé");
        }

        /// <summary>
        /// Récupération de tous les commentaires d'un utilisateur
        /// </summary>
        /// <param name="idUser">int : Identifiant utilisateur</param>
        /// <returns></returns>
        [HttpGet("idUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllCommentByUser(int idUser)
        {
            var commentByUser = await commentRepository.GetAllCommentByUser(idUser);

            if (commentByUser == null)
            {
                return NoContent();
            }

            return Ok(commentByUser);
        }

        /// <summary>
        /// Récupération de tous les commentaires d'un article
        /// </summary>
        /// <param name="idArticle">int : Identifiant de l'article</param>
        /// <returns></returns>
        [HttpGet("idArticle")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> GetAllCommentByArticle(int idArticle)
        {
            var commentByArticle = await commentRepository.GetAllCommentByUser(idArticle);

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
        [HttpGet("id")]
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
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<ActionResult> UpdateComment(UpdateCommentDTO commentDTO)
        {
            await commentRepository.UpdateComment(commentDTO);

            return Ok("Commemtaire modifié avec succes");
        }

        /// <summary>
        /// Suppression d'un commentaire
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await commentRepository.DeleteComment(id);

            return Ok("Commentaire supprimé");
        }




    }
}
