using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StreetTalk.Controllers;
using StreetTalk.Services;
using Xunit;

namespace StreetTalkTests.ControllerTests
{
    public class PublicPost : BaseTest
    {
        private PublicPostController Controller => CreateControllerWithLoggedInUser("19097530@student.hhs.nl", "19097530@student.hhs.nl");
        
        private PublicPostController CreateControllerWithLoggedInUser(string email, string username)
        {
            var userService = new Mock<IUserService>();
            var postService = new Mock<IPostService>().Object;
            var fileUploadService = new Mock<IFileUploadService>().Object;
                
            var seededDatabase = SeededCleanContext;
            var loggedInUser = seededDatabase.User.Single(u => u.Email == email);

            userService
                .Setup(u => u.GetCurrentlyLoggedInUsername())
                .Returns(username);
                
            userService.Setup(u => u.GetCurrentlyLoggedInUser())
                .Returns(loggedInUser);
                
            return new PublicPostController(seededDatabase, postService, userService.Object, fileUploadService);
        }
        
        [Fact]
        public void IndexReturnsView()
        {
            var result = Controller.Index(new PublicPostListFilters());
            Assert.IsType<ViewResult>(result);
        }
    }
}