using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Models;

namespace StreetTalk.Controllers
{
    public class PublicPostController : BaseController
    {
        public PublicPostController(StreetTalkContext context) : base(context) {}
        
        public IActionResult Index()
        {
            var posts = Db.Post.OfType<PublicPost>().Take(10).ToList();
            return View(posts);
        }
        
    }
}