using Microsoft.EntityFrameworkCore;
using StreetTalk.Models;

namespace StreetTalk
{
    public class StreetTalkContext : DbContext
    {
        
        public StreetTalkContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>().HasKey(like => new { like.userId, like.postId });
        }

        public DbSet<User> users { get; set; }

        public DbSet<Post> posts { get; set; }
    }
}