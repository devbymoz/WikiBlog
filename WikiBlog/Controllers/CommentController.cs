using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WikiBlog.Const;
using WikiBlog.DTOs.Articles;
using WikiBlog.DTOs.Comments;
using WikiBlog.Interfaces.IRepositories;
using WikiBlog.Models;
using WikiBlog.Repositories;

namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentRepository commentRepository;
        private UserManager<AppUser> userManager;
        private IUserRepository userRepository;
        public CommentController(ICommentRepository commentRepository, UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Création d'un nouveau commentaire
        /// </summary>
        /// <param name="commentDTO">Commentaire</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateComment(CreateCommentDTO commentDTO)
        {
            string? userConnectID = userManager.GetUserId(User);

            bool checkingCreation = await commentRepository.CreateComment(commentDTO, userRepository.GetUserId(userConnectID));

            if (checkingCreation == false)
            {
                return StatusCode(500);
            }
           // [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
            return Ok("Votre commentaire a bien été créé");
        }



        /// <summary>
        /// Récupération de la liste des commentaire avec leur auteur
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateComment(int id, UpdateCommentDTO commentDTO)
        {
            string? userConnectID = userManager.GetUserId(User);

            var appUser = await userManager.GetUserAsync(User);
            bool isAdmin = await userManager.IsInRoleAsync(appUser, "ADMIN");

            bool? checkingUpdate = await commentRepository.UpdateComment(id, commentDTO, userRepository.GetUserId(userConnectID), isAdmin);

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
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.USERCONNECT}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteComment(int id)
        {
            string? userConnectID = userManager.GetUserId(User);

            var appUser = await userManager.GetUserAsync(User);
            bool isAdmin = await userManager.IsInRoleAsync(appUser, "ADMIN");

            bool? checkingDelete = await commentRepository.DeleteComment(id, userRepository.GetUserId(userConnectID), isAdmin);

            if (checkingDelete == false)
            {
                return StatusCode(500);
            }

            return Ok("Commentaire supprimé");
        }
    }
}
