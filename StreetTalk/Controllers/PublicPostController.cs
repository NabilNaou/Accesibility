using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Models;
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

    [Authorize]
    public class PublicPostController : BaseController
    {
        private readonly PostService postService;
        private readonly UserService userService;

        public PublicPostController(StreetTalkContext context, PostService postService, UserService userService) : base(context)
        {
            this.postService = postService;
            this.userService = userService;
        }

        public IActionResult Index(PublicPostListFilters filters, int page = 1)
        {
            //TODO: Refactor this
            var perPage = 10;
            var skip = Math.Max(page - 1, 0) * perPage;

            var posts = Db.PublicPost
                .Where(p => !p.Closed || filters.ShowClosedPosts)
                .OrderBy(p => p.CreatedAt)
                .Skip(skip)
                .Take(perPage);

            var publicPostsWithLikes = posts.Select(a =>
                new PublicPostWithExtraData
                {
                    Post = a,
                    Liked = a.Likes.Any(b => b.UserId == userService.GetCurrentlyLoggedInUser()?.Id),
                    Reported = a.Reports.Any(b => b.UserId == userService.GetCurrentlyLoggedInUser()?.Id)
                }
            ).ToList();

            var viewModelData = new PublicPostViewModel
            {
                Posts = publicPostsWithLikes,
                Filters = filters
            };

            return View(viewModelData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PublicPost post)
        {
            if (!ModelState.IsValid) return View(post);

            var user = userService.GetCurrentlyLoggedInUser();
            post.UserId = user?.Id;
            user?.Posts.Add(post);

            Db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Post(int id)
        {
            try
            {
                ViewData["CurrentUserId"] = userService.GetCurrentlyLoggedInUser().Id;
                return View(postService.GetPublicPostById(id));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult PostLike(int id)
        {
            if (userService.GetCurrentlyLoggedInUser() == null)
                return Json(new PostJsonResult { Succes = false, Error = "U moet eerst inloggen" });

            try
            {
                var post = postService.GetPublicPostById(id);
                postService.ToggleLikeForPost(post, userService.GetCurrentlyLoggedInUser()?.Id);

                return Json(new PostJsonResult { Succes = true, NewLikes = post.Likes.Count() });
            }
            catch
            {
                return Json(new PostJsonResult { Succes = false, Error = "Wijziging kon niet worden opgeslagen" });
            }
        }

        [HttpPost]
        public IActionResult PostReport(int id)
        {
            if (userService.GetCurrentlyLoggedInUser() == null)
                return Json(new PostJsonResult { Succes = false, Error = "U moet eerst inloggen" });

            try
            {
                var post = postService.GetPublicPostById(id);

                if (postService.UserReportedPost(post, userService.GetCurrentlyLoggedInUser()?.Id))
                    return Json(new PostJsonResult { Succes = false, Error = "Je hebt deze post al gerapporteerd" });

                postService.AddReportForPost(post, userService.GetCurrentlyLoggedInUser()?.Id);
                return Json(new PostJsonResult { Succes = true });
            }
            catch
            {
                return Json(new PostJsonResult { Succes = false, Error = "Wijziging kon niet worden opgeslagen" });
            }
        }

        [HttpPost]
        public IActionResult PostComment(int id, string commentContent)
        {
            if (commentContent == null || commentContent == "") return RedirectToAction("Post", new { id });

            Comment postedComment = new Comment
            {
                Content = commentContent,
                AuthorId = userService.GetCurrentlyLoggedInUser()?.Id,
                PostId = id
            };
            postService.GetPublicPostById(id).Comments.Add(postedComment);
            Db.SaveChanges();


            return RedirectToAction("Post", new { id });
        }

        [HttpGet]
        public IActionResult EditComment(int id, int commentId)
        {
            ViewData["PublicPostId"] = id;
            return View(postService.GetPublicPostById(id).Comments.Single(c => c.Id == commentId));
        }

        [HttpPost]
        public IActionResult EditComment(int commentId, int id, string NewContent)
        {
            postService.GetPublicPostById(id).Comments.Single(c => c.Id == commentId).Content = NewContent;
            Db.SaveChanges();

            return RedirectToAction("Post", new { id });
        }

        public IActionResult DeleteComment(int id, int commentId)
        {
            postService.GetPublicPostById(id).Comments.RemoveAll(c => c.Id == commentId);
            Db.SaveChanges();


            return RedirectToAction("Post", new { id });
        }

    }
}