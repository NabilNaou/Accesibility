using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StreetTalk.Controllers;
using StreetTalk.Dtos;
using StreetTalk.Models;
using StreetTalk.Services;
using Xunit;

namespace StreetTalkTests.ControllerTests
{
    public class PublicPostTests : BaseTest
    {
        private PublicPostController Controller => CreateController("19097530@student.hhs.nl", "19097530@student.hhs.nl");
        
        private PublicPostController CreateController(string email = "", string username = "")
        {
            var userService = new Mock<IUserService>();
            var fileUploadService = new Mock<IFileUploadService>().Object;
            var seededDatabase = SeededCleanContext;
            var postService = new PostService(seededDatabase);
            var loggedInUser = seededDatabase.User.SingleOrDefault(u => u.Email == email);

            userService
                .Setup(u => u.GetCurrentlyLoggedInUsername())
                .Returns(username);
                
            userService.Setup(u => u.GetCurrentlyLoggedInUser())
                .Returns(loggedInUser);
            
            Console.WriteLine(userService.Object.GetCurrentlyLoggedInUser());
                
            return new PublicPostController(seededDatabase, postService, userService.Object, fileUploadService);
        }
        
        [Fact]
        public void IndexReturnsView()
        {
            var result = Controller.Index(new PublicPostListFilters());
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<PublicPostViewModel>(viewResult.Model);
        }

        [Fact]
        public void CreateReturnsView()
        {
            var result = Controller.Create();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<PostCategory>>(viewResult.ViewData["Categories"]);
        }

        [Fact]
        public void CreatePostSubmittedInvalidPost()
        {
            var c = Controller;
            c.ModelState.AddModelError("test", "test");
            var result = c.Create(new PublicPost()).Result;

            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void CreatePostSubmittedValidPost()
        {
            var result = Controller.Create(new PublicPost()).Result;
            
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        
        [Fact]
        public void CreatePostSubmittedValidPostWithPhoto()
        {
            var photo = new Mock<IFormFile>();
            photo.Setup(p => p.Length).Returns(1);
            
            var result = Controller.Create(new PublicPost
            {
                UploadedPhoto = photo.Object,
                UploadedPhotoIsSensitive = true
            }).Result;
            
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void PostReturnsView()
        {
            var result = Controller.Post(1);
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void PostDoesntExist()
        {
            var result = Controller.Post(2425324);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void PostNotLoggedIn()
        {
            var controller = CreateController();
            var result = controller.Post(1);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        //Post like should always return a valid json result.
        [Fact]
        public void PostLikeNotLoggedIn()
        {
            var c = CreateController();
            var result = c.PostLike(1);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public void PostLikeLoggedIn()
        {
            var result = Controller.PostLike(1);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public void PostLikeDuplicate()
        {
            var controller = Controller;
            controller.PostLike(1);
            var result = controller.PostLike(1);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public void PostLikeInvalidPost()
        {
            var result = Controller.PostLike(12414242);
            Assert.IsType<JsonResult>(result);
        }
        
        //Post report should always return a valid json result.
        [Fact]
        public void PostReportNotLoggedIn()
        {
            var c = CreateController();
            var result = c.PostReport(1);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public void PostReportLoggedIn()
        {
            var result = Controller.PostReport(1);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public void PostReportDuplicate()
        {
            var controller = Controller;
            controller.PostReport(1);
            var result = controller.PostReport(1);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public void PostReportInvalidPost()
        {
            var result = Controller.PostReport(12414242);
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void PostComment()
        {
            var result = Controller.PostComment(1, "Hello, world!");
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Post", redirect.ActionName);
            Assert.Equal(1, (int)redirect.RouteValues["id"]);
        }
        
        [Fact]
        public void PostCommentEmpty()
        {
            var result = Controller.PostComment(1, null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Post", redirect.ActionName);
            Assert.Equal(1, (int)redirect.RouteValues["id"]);
        }

        [Fact]
        public void CensorComment()
        {
            var result = Controller.CensorComment(1, 1);
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void CensorCommentSubmitted()
        {
            var result = Controller.CensorComment(1, 1, "Censored!");
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Post", redirect.ActionName);
            Assert.Equal(1, (int)redirect.RouteValues["id"]);
        }

        [Fact]
        public void PostEdit()
        {
            var result = Controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void PostEditSubmittedNotOwned()
        {
            var result = Controller.Edit(new PublicPost(), 3);
            var redirect = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        
        [Fact]
        public void PostEditSubmittedOwned()
        {
            var result = Controller.Edit(new PublicPost
            {
                Title = "Test",
                Content = "TestTestTest"
            }, 1);
            var redirect = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Post", redirect.ActionName);
        }
        
        [Fact]
        public void PostEditSubmittedInvalidData()
        {
            var result = Controller.Edit(new PublicPost
            {
                Title = "a",
                Content = "TestTestTest"
            }, 1);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void PostDeleteNotOwned()
        {
            var controller = CreateController("gemeentemedewerker@streettalk.nl", "test");
            var result = controller.Delete(1);
            var redirect = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        
        [Fact]
        public void PostDeleteOwned()
        {
            var result = Controller.Delete(1);
            var redirect = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        
        [Fact]
        public void PostClose()
        {
            var result = Controller.Close(1);
            var redirect = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        
        [Fact]
        public void PostCheckPostTitleSimilarityPositive()
        {
            //Gedeeltelijke match op "Overlast Touristen"
            var result = Controller.CheckPostTitleSimilarity("Overlast Tourist");
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
        
        [Fact]
        public void PostCheckPostTitleSimilarityNegative()
        {
            var result = Controller.CheckPostTitleSimilarity("Te weinig natuur");
            Assert.IsAssignableFrom<NoContentResult>(result);
        }
        
        [Fact]
        public void PostCheckPostTitleSimilarityInvalid()
        {
            var result = Controller.CheckPostTitleSimilarity(null);
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }
    }
}