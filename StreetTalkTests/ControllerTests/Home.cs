using System;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Controllers;
using Xunit;

namespace StreetTalkTests.ControllerTests
{
    public class Home : BaseTest
    {
        private HomeController Controller => new HomeController(Db);
        
        [Fact]
        public void IndexReturnsView()
        {
            var result = Controller.Index();
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void PrivacyReturnsView()
        {
            var result = Controller.Privacy();
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void SiteMapReturnsView()
        {
            var result = Controller.SiteMap();
            Assert.IsType<ViewResult>(result);
        }
    }
}