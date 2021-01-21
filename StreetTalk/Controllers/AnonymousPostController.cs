﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Data;
using StreetTalk.Models;
using StreetTalk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StreetTalk.Controllers
{
    [Authorize]

    public class AnonymousPostController : BaseController
    {
        private readonly UserService userService;
        public AnonymousPostController(StreetTalkContext context, UserService userService): base(context) {
            this.userService = userService;
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
            var currentuser = userService.GetCurrentlyLoggedInUser();
            anoniemeMelding.Pseudonym = Db.Encrypt(currentuser.Email);
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
