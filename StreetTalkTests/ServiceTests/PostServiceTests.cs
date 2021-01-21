using StreetTalk.Services;
using Xunit;

namespace StreetTalkTests.ServiceTests
{
    public class PostServiceTests : BaseTest
    {
        private PostService PostService => new PostService(SeededCleanContext);
        
        [Fact]
        public void UserViewedPostNegative()
        {
            var postService = PostService;
            var viewed = postService.UserViewedPost("test", postService.GetPublicPostById(1));
            Assert.False(viewed);
        }
        
        [Fact]
        public void UserViewedPostPositive()
        {
            var postService = PostService;
            postService.AddView("test", postService.GetPublicPostById(1));
            var viewed = postService.UserViewedPost("test", postService.GetPublicPostById(1));
            Assert.True(viewed);
        }
    }
}