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

        public IActionResult CreatePublicPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePublicPost(PublicPost post)
        {
            if (ModelState.IsValid)
            {
                //TODO Maak ingelogde gebruiker
                User user = Db.User.First();
                post.User = user;
                user.Posts.Add(post);
                //Db.Post.Add(post);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public IActionResult Post(int id)
        {
            var post = Db.PublicPost.SingleOrDefault(p => p.Id == id);
            
            return View(post);
        }
        
    }
}