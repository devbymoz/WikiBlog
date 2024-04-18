using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.Models;
using Microsoft.EntityFrameworkCore;
using WikiBlog.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WikiBlog.Const;
using WikiBlog.DTOs.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using WikiBlog.Config;



namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        UserManager<AppUser> userManager;
        SignInManager<AppUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        DbContextWikiBlog dbContextWikiBlog;
        public UserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            DbContextWikiBlog dbContextWikiBlog)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.dbContextWikiBlog = dbContextWikiBlog;
        }

        /// <summary>
        /// Inscription d'un utilisateur avec un rôle USERCONNECT
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateUser(NewUserDTO userDTO)
        {
            bool checkAge = UserService.IsMajor(userDTO.DateOfBirth);

            if (checkAge)
            {
                return Problem("Vous devez avoir 18 ans");
            }

            var appUser = new AppUser
            {
                UserName = userDTO.Login,
                DateOfBirth = userDTO.DateOfBirth,
                Email = userDTO.Login,
                NormalizedEmail = userDTO.Login.ToUpperInvariant()
            }; // Add other properties if needed

            var result = await userManager.CreateAsync(appUser, userDTO.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(appUser, Roles.USERCONNECT);

                User user = new User { AppUserId = appUser.Id };
                appUser.User = user;

                await dbContextWikiBlog.Users.AddAsync(user);
                await dbContextWikiBlog.SaveChangesAsync();

                // use appUser to create other item with you context if needed
                return Ok("Utilisateur créé");
            }
            else
                return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        /// <summary>
        /// Passer un utilisateur existant en role Admin
        /// </summary>
        /// <param name="email">string : Email de l'utilisateur</param>
        /// <returns></returns>
        [HttpPost("{email}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddUserToRoleAdmin(string email)
        {
            var appUser = await userManager.FindByEmailAsync(email);

            if (appUser == null)
            {
                return NoContent();
            }

            await userManager.AddToRoleAsync(appUser, "ADMIN");

            return Ok();
        }

        /// <summary>
        /// Création de nouveau role
        /// </summary>
        /// <param name="roleName">string : Nom du role</param>
        /// <returns></returns>
        [HttpPost("{roleName}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            string roleNameCapitalise = roleName.ToUpperInvariant();

            if (!await roleManager.RoleExistsAsync(roleNameCapitalise))
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = roleNameCapitalise, NormalizedName = roleNameCapitalise });

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                    return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
            }

            return Ok();
        }











        //[HttpGet]
        //public IActionResult GetUserName()
        //{
        //    return Ok($"{User.Identity.Name}");
        //}

        //[HttpGet]
        //public IActionResult GetUserId()
        //{
        //    return Ok($"{userManager.GetUserId(User)}");
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetAppUser()
        //{
        //    var appUser = await userManager.GetUserAsync(User);
        //    return Ok($"{appUser.Email}");
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAnAppUserById(string userId)
        //{
        //    var appUser = await userManager.Users
        //                            .FirstOrDefaultAsync(u => u.Id == userId);

        //    return Ok($"{appUser.Email}");
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ChangePassword(ChangePasswordDTO userDTO)
        //{
        //    var appUser = await userManager.GetUserAsync(User);

        //    var result = await userManager.ChangePasswordAsync(appUser, userDTO.CurrentPassword, userDTO.NewPassword);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //    else
        //        return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
        //}

        //[HttpGet]
        //public async Task<IActionResult> IsUserInRole()
        //{
        //    var appUser = await userManager.GetUserAsync(User);

        //    return Ok($"{userManager.IsInRoleAsync(appUser, "ADMIN")}");
        //}

        //[HttpGet]
        //public async Task<IActionResult> AnonymousRoute()
        //{
        //    return Ok($"Anonymous");
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> SecureRoute()
        //{
        //    return Ok($"Logged");
        //}

        //[HttpGet]
        //[Authorize(Roles = "ADMIN")]
        //public async Task<IActionResult> SecureRouteForRole()
        //{
        //    return Ok($"Logged with admin role");
        //}





    }






}
