using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WikiBlog.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }                
    }
}
