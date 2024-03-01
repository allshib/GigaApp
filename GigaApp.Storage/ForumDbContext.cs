using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage
{
    public class ForumDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) {

            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
