using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Paylap.Entities;
using System.Collections.Generic;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Paylap.DataAccess
{
    public class PaylapDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=MUSAC; Database=PaylapDB; uid=sa; pwd=password1; TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<BookMark> BookMarks { get; set; }

    }
}