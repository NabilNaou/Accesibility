using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetTalk.Models
{
    public class MakingAnonymousPost : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
