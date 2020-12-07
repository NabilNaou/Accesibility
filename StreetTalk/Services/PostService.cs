using System;
using System.Linq;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Services
{
    public class PostService
    {
        private readonly StreetTalkContext Db;

        public PostService(StreetTalkContext context)
        {
            Db = context;
        }

        public PublicPost GetPublicPostById(int id)
        {
            return Db.PublicPost.Single(p => p.Id == id);
        }

        public bool UserLikedPost(PublicPost post, int userId)
        {
            return post.Likes.Any(b => b.UserId == userId);
        }

        public void RemoveLikeFromPost(PublicPost post, int userId)
        {
            post.Likes.RemoveAll(b => b.UserId == userId);

            Db.SaveChanges();
        }

        public void AddLikeForPost(PublicPost post, int userId)
        {
            var like = new Like
            {
                Post = post,
                UserId = userId
            };

            post.Likes.Add(like);

            Db.SaveChanges();
        }

        public void ToggleLikeForPost(PublicPost post, int userId)
        {
            if (UserLikedPost(post, userId))
                RemoveLikeFromPost(post, userId);
            else
                AddLikeForPost(post, userId);
        }
        
        public bool UserReportedPost(PublicPost post, int userId)
        {
            return post.Reports.Any(b => b.UserId == userId);
        }
        
        public void AddReportForPost(PublicPost post, int userId)
        {
            var report = new Report
            {
                Post = post,
                UserId = userId
            };

            post.Reports.Add(report);

            Db.SaveChanges();
        }
    }
}