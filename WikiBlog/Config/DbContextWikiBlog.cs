using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WikiBlog.Models;

namespace WikiBlog.Config
{
    public class DbContextWikiBlog : IdentityDbContext<AppUser>
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User user1 = new User { Id = 1 };
            User user2 = new User { Id = 2 };
            modelBuilder.Entity<User>().HasData(new List<User> { user1, user2 });

            base.OnModelCreating(modelBuilder);
        }


    }
}
