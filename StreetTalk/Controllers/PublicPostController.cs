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
            var skip = Math.Max((page - 1) * perPage, 1);
            var posts = Db.Post.OfType<PublicPost>().OrderBy(p => p.CreatedAt).Skip(skip).Take(perPage).ToList();
            
            return View(posts);
        }
        
    }
}