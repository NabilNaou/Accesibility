using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Models;

namespace StreetTalk.Controllers
{
    [Serializable()]
    public class PostLikeResult
    {
        public bool Succes { get; set; }
        public string Error { get; set; } = "";
        public int NewLikes { get; set; }
    }

    public class PublicPostViewModel
    {
        public PublicPost Post { get; set; }
        public bool Liked { get; set; }
    }

    public class PublicPostController : BaseController
    {
        public PublicPostController(StreetTalkContext context) : base(context) {}
        
        public IActionResult Index(int page = 1) //TODO: Replace hardcoded user id with currently logged in user id
        {
            var perPage = 10;
            var skip = Math.Max(page - 1, 0) * perPage;
            var posts = Db.PublicPost
                .OrderBy(p => p.CreatedAt)
                .Skip(skip)
                .Take(perPage)
                .Select(a => new PublicPostViewModel {
                    Post = a, 
                    Liked = a.Likes.Any(b => b.UserId == 2)
                })
                .ToList();
            
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = Db.PublicPost.SingleOrDefault(p => p.Id == id);
            
            return View(post);
        }

        [HttpPost]
        public IActionResult PostLike(int id) //TODO: Replace hardcoded user id with currently logged in user id
        {
            var post = Db.PublicPost.SingleOrDefault(p => p.Id == id);

            if(post == null)
            {
                return Json(new PostLikeResult { Succes = false, Error = "Deze post bestaat niet."});
            }

            if (post.Likes.Any(b => b.UserId == 2))
            {
                post.Likes.RemoveAll(b => b.UserId == 2);
            }
            else
            {
                var like = new Like
                {
                    Post = post,
                    User = Db.User.Single(u => u.Id == 2)
                };

                post.Likes.Add(like);
            }

            try
            {
                Db.SaveChanges();
            }
            catch
            {
                return Json(new PostLikeResult { Succes = false, Error = "Wijziging kon niet worden opgeslagen" });
            }

            return Json(new PostLikeResult { Succes = true, NewLikes = post.Likes.Count()});
        }

    }
}