using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StreetTalk.Controllers;
using StreetTalk.Services;
using StreetTalkTests.Mocks;
using Xunit;

namespace StreetTalkTests.ControllerTests
{
    public class ProfileTests : BaseTest
    {
        private ProfileController Controller => CreateController("moderator@streettalk.nl", "Moderator");

        private ProfileController CreateController(string email = "", string username = "")
        {
            var signInManager = new FakeSignInManager();
            var userService = new Mock<IUserService>();
                
            var seededDatabase = SeededCleanContext;
            var loggedInUser = seededDatabase.User.SingleOrDefault(u => u.Email == email);

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
        public void IndexNotLoggedIn()
        {
            var controller = CreateController();
            var result = controller.Index();
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void DeleteAccountReturnsView()
        {
            var result = Controller.DeleteAccount();
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void DeleteAccountNotLoggedIn()
        {
            var controller = CreateController();
            var result = controller.DeleteAccount("test").Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void DeleteAccountSubmittedInvalidEmail()
        {
            var controller = CreateController("19097530@student.hhs.nl", "19097530@student.hhs.nl");
            var result = controller.DeleteAccount("moderator@streettalk.nl").Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void DeleteAccountSubmittedCorrectEmail()
        {
            var controller = CreateController("19097530@student.hhs.nl", "19097530@student.hhs.nl");
            var result = controller.DeleteAccount("19097530@student.hhs.nl").Result;
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void FetchPersonalDataNotLoggedIn()
        {
            var controller = CreateController();
            var result = controller.FetchPersonalData();
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void FetchPersonalDataLoggedIn()
        {
            var result = Controller.FetchPersonalData();
            Assert.IsType<FileContentResult>(result);
        }

        [Fact]
        public void UpdateProfile()
        {
            var result = Controller.Index(new StreetTalk.Models.Profile());
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        
        [Fact]
        public void UpdateProfileNotLoggedIn()
        {
            var controller = CreateController();
            var result = controller.Index(new StreetTalk.Models.Profile());
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void UpdateProfileInvalidData()
        {
            var controller = CreateController("19097530@student.hhs.nl", "bla");
            controller.ModelState.AddModelError("test", "test");
            var result = controller.Index(new StreetTalk.Models.Profile());
            Assert.IsType<ViewResult>(result);
        }
    }
}