using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Models;

namespace StreetTalk.Controllers
{
    public class PublicPostController : BaseController
    {
        public PublicPostController(StreetTalkContext context) : base(context) {}
        
        public IActionResult Index(int page = 1)
        {
            var perPage = 10;
            var skip = Math.Max(page - 1, 0) * perPage;
            var posts = Db.PublicPost.OrderBy(p => p.CreatedAt).Skip(skip).Take(perPage).ToList();
            
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = Db.PublicPost.SingleOrDefault(p => p.Id == id);
            
            return View(post);
        }

        [HttpPost]
        public IActionResult PostLike(int id)
        {
            var post = Db.PublicPost.SingleOrDefault(p => p.Id == id);

            if(post == null)
            {
                return Json(new { succes = false });
            }

            var like = new Like { 
                Post = post,
                User = Db.User.Single(u => u.Id == 2)
            };

            post.Likes.Add(like);

            try
            {
                Db.SaveChanges();
            }
            catch
            {
                return Json(new { succes = false });
            }

            return Json(new { succes = true });
        }

    }
}