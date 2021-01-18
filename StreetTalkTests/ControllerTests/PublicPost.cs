using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using StreetTalk.Controllers;
using StreetTalk.Services;
using StreetTalkTests.Mocks;
using Xunit;

namespace StreetTalkTests.ControllerTests
{
    public class PublicPost : BaseTest
    {
        private PublicPostController Controller => CreateControllerWithLoggedInUser("", "");
        
        private PublicPostController CreateControllerWithLoggedInUser(string email, string username)
        {
            var userService = new Mock<IUserService>();
            var postService = new Mock<IPostService>().Object;
            var config = new Mock<IConfiguration>().Object;
            var environment = new Mock<IWebHostEnvironment>().Object;
                
            var seededDatabase = SeededCleanContext;
            var loggedInUser = seededDatabase.User.Single(u => u.Email == email);

            userService
                .Setup(u => u.GetCurrentlyLoggedInUsername())
                .Returns(username);
                
            userService.Setup(u => u.GetCurrentlyLoggedInUser())
                .Returns(loggedInUser);
                
            return new PublicPostController(seededDatabase, postService, userService.Object, config, environment);
        }
        
        [Fact]
        public void IndexReturnsView()
        {
            //var result = Controller.Index();
            //Assert.IsType<ViewResult>(result);
        }
    }
}