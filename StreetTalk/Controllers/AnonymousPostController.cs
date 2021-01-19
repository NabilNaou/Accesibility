using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Data;
using StreetTalk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StreetTalk.Controllers
{
    [Authorize]

    public class AnonymousPostController : BaseController
    {
        public AnonymousPostController(StreetTalkContext context): base(context) { 
            
        }
        
        [Authorize(Roles = "Administrator, Gemeentemedewerker")]
        public IActionResult Index()
        {
            return View(Db.AnonymousPost);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(AnonymousPost anoniemeMelding)
        {
            if (!ModelState.IsValid ) {
                return View();
            }
            
            Db.AnonymousPost.Add(anoniemeMelding);
            Db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize(Roles = "Administrator, Gemeentemedewerker")]
        public IActionResult Content(int id)
        {
            return View(Db.AnonymousPost.Single(post => post.Id == id));
        }
    }
}
