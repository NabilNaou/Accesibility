using System;
using System.Collections.Generic;
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

        public bool UserLikedPost(PublicPost post, string userId)
        {
            return post.Likes.Any(b => b.UserId == userId);
        }

        public void RemoveLikeFromPost(PublicPost post, string userId)
        {
            post.Likes.RemoveAll(b => b.UserId == userId);

            Db.SaveChanges();
        }

        public void AddLikeForPost(PublicPost post, string userId)
        {
            var like = new Like
            {
                Post = post,
                UserId = userId
            };

            post.Likes.Add(like);

            Db.SaveChanges();
        }

        public void ToggleLikeForPost(PublicPost post, string userId)
        {
            if (UserLikedPost(post, userId))
                RemoveLikeFromPost(post, userId);
            else
                AddLikeForPost(post, userId);
        }
        
        public bool UserReportedPost(PublicPost post, string userId)
        {
            return post.Reports.Any(b => b.UserId == userId);
        }
        
        public void AddReportForPost(PublicPost post, string userId)
        {
            var report = new Report
            {
                Post = post,
                UserId = userId
            };

            post.Reports.Add(report);

            Db.SaveChanges();
        }

        public void DeletePostById(int id)
        {
            var post = GetPublicPostById(id);

            Db.PublicPost.Remove(post);

            Db.SaveChanges();
        }

        public IEnumerable<string> GetRecentTitles()
        {
            return Db.PublicPost.ToList()
                .Where(p => (p.CreatedAt!.Value - DateTime.Now).TotalDays < 30)
                .Select(p => p.Title)
                .AsEnumerable();
        }

        public PublicPost EditPostById(int id, PublicPost newpost)
        {
            var post = GetPublicPostById(id);

            post.Title = newpost.Title;
            post.Content = newpost.Content;

            return post;
        }
        public void AddView(String userid, PublicPost post)
        {
            var view = new View
            {
                Post = post,
                UserId = userid
            };
            post.Views.Add(view);
            Db.SaveChanges();
        }

        public bool UserViewedPost(String userid, PublicPost post)
        {
            return post.Views.Any(view => view.UserId == userid);
        }
    }
}