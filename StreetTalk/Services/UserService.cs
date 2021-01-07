using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StreetTalk.Models;

namespace StreetTalk.Services
{
    public class UserService
    {
        private readonly UserManager<StreetTalkUser> userManager;
        private readonly IHttpContextAccessor httpContext;

        public UserService(UserManager<StreetTalkUser> userManager, IHttpContextAccessor httpContext)
        {
            this.userManager = userManager;
            this.httpContext = httpContext;
        }

        public StreetTalkUser GetCurrentlyLoggedInUser()
        {
            return userManager.GetUserAsync(httpContext.HttpContext.User).Result;
        }

        public string GetCurrentlyLoggedInUsername()
        {
            return GetCurrentlyLoggedInUser().Profile.FullName;
        }
    }
}