using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(StreetTalkContext context) : base(context) {}
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SiteMap()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}