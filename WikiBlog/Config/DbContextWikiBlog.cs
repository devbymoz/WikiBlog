using Microsoft.EntityFrameworkCore;
using System;
using WikiBlog.Models;

namespace WikiBlog.Config
{
    public class DbContextWikiBlog : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbContextWikiBlog(DbContextOptions DbContextOptions) : base(DbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseInMemoryDatabase("ExoDbInMemory");
                optionsBuilder.LogTo(Console.WriteLine);
            }
        }


    }
}
