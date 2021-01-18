using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StreetTalk.Controllers;
using StreetTalk.Services;
using StreetTalkTests.Mocks;
using Xunit;

namespace StreetTalkTests.ControllerTests
{
    public class Profile : BaseTest
    {
        private ProfileController Controller => CreateControllerWithLoggedInUser("moderator@streettalk.nl", "Moderator");

        private ProfileController CreateControllerWithLoggedInUser(string email, string username)
        {
            var signInManager = new FakeSignInManager();
            var userService = new Mock<IUserService>();
                
            var seededDatabase = SeededCleanContext;
            var loggedInUser = seededDatabase.User.Single(u => u.Email == email);

            userService
                .Setup(u => u.GetCurrentlyLoggedInUsername())
                .Returns(username);
                
            userService.Setup(u => u.GetCurrentlyLoggedInUser())
                .Returns(loggedInUser);
                
            return new ProfileController(seededDatabase, userService.Object, signInManager);
        }
        
        [Fact]
        public void IndexReturnsView()
        {
            var result = Controller.Index();
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void DeleteAccountReturnsView()
        {
            var result = Controller.DeleteAccount();
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void DeleteAccountSubmittedInvalidEmail()
        {
            var controller = CreateControllerWithLoggedInUser("19097530@student.hhs.nl", "19097530@student.hhs.nl");
            var result = controller.DeleteAccount("moderator@streettalk.nl").Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void DeleteAccountSubmittedCorrectEmail()
        {
            var controller = CreateControllerWithLoggedInUser("19097530@student.hhs.nl", "19097530@student.hhs.nl");
            var result = controller.DeleteAccount("19097530@student.hhs.nl").Result;
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}