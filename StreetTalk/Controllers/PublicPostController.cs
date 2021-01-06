using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using StreetTalk.Models;
using StreetTalk.Data;
using StreetTalk.Services;

namespace StreetTalk.Controllers
{
    [Serializable]
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
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment environment;
        private readonly string[] permittedUploadExtensions = { ".png", ".jpg", ".jpeg" };

        public PublicPostController(StreetTalkContext context, PostService postService, UserService userService,
            IConfiguration config, IWebHostEnvironment environment) : base(context)
        {
            this.postService = postService;
            this.userService = userService;
            this.config = config;
            this.environment = environment;
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
            IEnumerable<PostCategory> categories = Db.PostCategory.ToList();
            ViewData["categories"] = categories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublicPost post)
        {
            if (!ModelState.IsValid) return View(post);

            //Photo upload
            if (post.UploadedPhoto != null && post.UploadedPhoto.Length > 0)
            {
                var extenstion = Path.GetExtension(post.UploadedPhoto.FileName);
                if (extenstion == null || !permittedUploadExtensions.Contains(extenstion))
                    return View(post);

                var newFilename = Path.GetRandomFileName() + extenstion;
                var filePath = Path.Combine(config["StoredFilesPath"], newFilename);
                var uploadsPath = Path.Combine(environment.WebRootPath, config["StoredFilesPath"]);

                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                await using var stream = System.IO.File.Create(Path.Combine(environment.WebRootPath, filePath));
                await post.UploadedPhoto.CopyToAsync(stream);

                post.Photo = new PostPhoto
                {
                    Sensitive = post.UploadedPhotoIsSensitive,
                    Photo = new Photo
                    {
                        Filename = "/" + filePath
                    }
                };
            }

            var user = userService.GetCurrentlyLoggedInUser();
            post.UserId = user?.Id;
            user?.Posts.Add(post);

            await Db.SaveChangesAsync();

            return RedirectToAction("Post", new { id = post.Id });
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

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(PublicPost post, int id)
        {
            if (!ModelState.IsValid) return View(post);


            var user = userService.GetCurrentlyLoggedInUser();
            post.UserId = user?.Id;
            user?.Posts.Add(post);

            Db.SaveChanges();

            return RedirectToAction("Post", new { id = post.Id });
        }
    }
}