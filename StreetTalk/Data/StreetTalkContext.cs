using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StreetTalk.Models;

namespace StreetTalk.Data
{
    public class StreetTalkContext : DbContext
    {
        
        public StreetTalkContext(DbContextOptions options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>().HasKey(like => new { like.UserId, like.PostId });
            modelBuilder.Entity<PostPhoto>().HasKey(photo => new { photo.PhotoId, photo.PostId });
            modelBuilder.Entity<ProfilePhoto>().HasKey(photo => new { photo.PhotoId, photo.ProfileId });
        }

        public DbSet<User> User { get; set; }

        public DbSet<Post> Post { get; set; }

        public IEnumerable<PublicPost> PublicPost => Post.OfType<PublicPost>();
        public IEnumerable<AnonymousPost> AnonymousPost => Post.OfType<AnonymousPost>();
    }
}