using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Data;
using StreetTalk.Models;
using StreetTalk.Services;

namespace StreetTalk.Controllers
{
    [Serializable()]
    public class PostJsonResult
    {
        public bool Succes { get; set; }
        public string Error { get; set; } = "";
        public int NewLikes { get; set; }
    }

    public class PublicPostWithExtraData
    {
        public PublicPost Post { get; set; }
        public bool Liked { get; set; }
        
        public bool Reported { get; set; }
    }

    public class PublicPostViewModel
    {
        public List<PublicPostWithExtraData> Posts { get; set; }
        public PublicPostListFilters Filters { get; set; }
    }

    public class PublicPostListFilters
    {
        public bool ShowClosedPosts { get; set; } = false;
    }

    public class PublicPostController : BaseController
    {
        private readonly PostService postService;

        public PublicPostController(StreetTalkContext context, PostService postService) : base(context)
        {
            this.postService = postService;
        }

        public IActionResult Index(PublicPostListFilters filters, int page = 1) //TODO: Replace hardcoded user id with currently logged in user id
        {
            //TODO: Refactor this
            var perPage = 10;
            var skip = Math.Max(page - 1, 0) * perPage;

            var posts = Db.PublicPost
                .OrderBy(p => p.CreatedAt)
                .Where(p => !p.Closed || filters.ShowClosedPosts)
                .Skip(skip)
                .Take(perPage);

            var publicPostsWithLikes = posts.Select(a =>
                new PublicPostWithExtraData
                {
                    Post = a,
                    Liked = a.Likes.Any(b => b.UserId == "2"),
                    Reported = a.Reports.Any(b => b.UserId == "2")
                }
            ).ToList();

            var viewModelData = new PublicPostViewModel
            {
                Posts = publicPostsWithLikes,
                Filters = filters
            };

            return View(viewModelData);
        }

        public IActionResult Post(int id)
        {
            try
            {
                return View(postService.GetPublicPostById(id));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult PostLike(int id) //TODO: Replace hardcoded user id with currently logged in user id
        {
            try
            {
                var post = postService.GetPublicPostById(id);
                postService.ToggleLikeForPost(post, "2");
                
                return Json(new PostJsonResult {Succes = true, NewLikes = post.Likes.Count()});
            }
            catch
            {
                return Json(new PostJsonResult {Succes = false, Error = "Wijziging kon niet worden opgeslagen"});
            }
        }
        
        [HttpPost]
        public IActionResult PostReport(int id) //TODO: Replace hardcoded user id with currently logged in user id
        {
            try
            {
                var post = postService.GetPublicPostById(id);
                
                if (postService.UserReportedPost(post, "2"))
                    return Json(new PostJsonResult {Succes = false, Error = "Je hebt deze post al gerapporteerd"});

                postService.AddReportForPost(post, "2");
                return Json(new PostJsonResult {Succes = true});
            }
            catch
            {
                return Json(new PostJsonResult {Succes = false, Error = "Wijziging kon niet worden opgeslagen"});
            }
        }
    }
}