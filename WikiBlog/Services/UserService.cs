using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WikiBlog.Config;
using WikiBlog.Models;

namespace WikiBlog.Services
{
    public class UserService
    {
        public static bool IsMajor(DateTime date)
        {
            int age = DateTime.Now.Year - date.Year;

            if (age < 18)
            {
                return false;
            }

            return true;
        }
    }
}
