using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HasCategory>()
                .HasKey(o => new { o.NewsId, o.CategoryId });
        }

        public DbSet<News> News { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<HasCategory> HasCategories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}
