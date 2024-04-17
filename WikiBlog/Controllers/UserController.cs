using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WikiBlog.Models;
using Microsoft.EntityFrameworkCore;
using WikiBlog.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WikiBlog.Const;



namespace WikiBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<AppUser> userManager;
        SignInManager<AppUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        public UserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult GetUserName()
        {
            return Ok($"{User.Identity.Name}");
        }


        [HttpGet]
        public IActionResult GetUserId()
        {
            return Ok($"{userManager.GetUserId(User)}");
        }


        [HttpGet]
        public async Task<IActionResult> GetAppUser()
        {
            var appUser = await userManager.GetUserAsync(User);
            return Ok($"{appUser.Email}");
        }


        [HttpGet]
        public async Task<IActionResult> GetAnAppUserById(string userId)
        {
            var appUser = await userManager.Users
                                    .FirstOrDefaultAsync(u => u.Id == userId);

            return Ok($"{appUser.Email}");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(NewUserDTO userDTO)
        {
            bool checkAge = UserService.IsMajor(userDTO.DateOfBirth);

            if (checkAge)
            {
                return Problem("Vous devez avoir 18 ans");
            }

            var appUser = new AppUser { 
                UserName = userDTO.Login,
                DateOfBirth = userDTO.DateOfBirth,
            }; // Add other properties if needed

            var result = await userManager.CreateAsync(appUser, userDTO.Password);
            roleManager.
            if (result.Succeeded)
            {
                var userConnectRole = roleManager.FindByNameAsync(Roles.USERCONNECT).Result;

                if (userConnectRole != null)
                {
                    IdentityResult roleresult = await roleManager.AddToRoleAsync(appUser, userConnectRole.Name);
                }

                // use appUser to create other item with you context if needed
                return Ok("Utilisateur créé");
            }
            else
                return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO userDTO)
        {
            var appUser = await userManager.GetUserAsync(User);

            var result = await userManager.ChangePasswordAsync(appUser, userDTO.CurrentPassword, userDTO.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
                return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateRoleAdmin()
        {
            if (!await roleManager.RoleExistsAsync("ADMIN"))
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" });

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                    return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));

            }

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> AddUserToRoleAdmin()
        {
            var appUser = await userManager.GetUserAsync(User);

            await userManager.AddToRoleAsync(appUser, "ADMIN");

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> IsUserInRole()
        {
            var appUser = await userManager.GetUserAsync(User);

            return Ok($"{userManager.IsInRoleAsync(appUser, "ADMIN")}");
        }


        [HttpGet]
        public async Task<IActionResult> AnonymousRoute()
        {
            return Ok($"Anonymous");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SecureRoute()
        {
            return Ok($"Logged");
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SecureRouteForRole()
        {
            return Ok($"Logged with admin role");
        }
    }







    public class LoginUserDTO
    {
        public string Login { get; internal set; }
        public string Password { get; internal set; }
        public bool RememberMe { get; internal set; }
    }

    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; internal set; }
        public string NewPassword { get; internal set; }
    }

    public class NewUserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }

    }



}
