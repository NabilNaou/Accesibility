using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;
using StreetTalk.Models;

namespace StreetTalk.Data
{
    public class StreetTalkContext : IdentityDbContext<StreetTalkUser>
    {
        private readonly IEncryptionProvider encryptionProvider;

        public StreetTalkContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            var encryptionKey = Convert.FromBase64String(configuration.GetValue<string>("DatabaseEncryptionKey"));
            var encryptionIv = Convert.FromBase64String(configuration.GetValue<string>("DatabaseEncryptionIV"));
            encryptionProvider = new AesProvider(encryptionKey, encryptionIv);
        }
        
        public StreetTalkContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            if(encryptionProvider != null)
                modelBuilder.UseEncryption(encryptionProvider);
            
            modelBuilder.Entity<View>().HasKey(view => new { view.UserId, view.PostId });
            modelBuilder.Entity<Like>().HasKey(like => new { like.UserId, like.PostId });
            modelBuilder.Entity<Report>().HasKey(report => new { report.UserId, report.PostId });
            modelBuilder.Entity<PostPhoto>().HasKey(photo => new { photo.PhotoId, photo.PostId });
            modelBuilder.Entity<ProfilePhoto>().HasKey(photo => new { photo.PhotoId, photo.ProfileId });
        }

        public DbSet<StreetTalkUser> User { get; set; }

        public DbSet<Post> Post { get; set; }
        
        public DbSet<PostCategory> PostCategory { get; set; }

        public IEnumerable<PublicPost> PublicPost => Post.OfType<PublicPost>();
        public IEnumerable<AnonymousPost> AnonymousPost => Post.OfType<AnonymousPost>();
    }
}
