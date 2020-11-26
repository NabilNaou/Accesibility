using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
        
    }
}