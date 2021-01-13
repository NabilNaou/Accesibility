using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using StreetTalk.Models;

namespace StreetTalk.Services
{
    public class UserService
    {
        private readonly UserManager<StreetTalkUser> userManager;
        private readonly StreetTalkSignInManager signInManager;
        private readonly IHttpContextAccessor httpContext;

        public UserService(UserManager<StreetTalkUser> userManager, StreetTalkSignInManager signInManager, IHttpContextAccessor httpContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContext = httpContext;
        }

        public StreetTalkUser GetCurrentlyLoggedInUser()
        {
            var user = userManager.GetUserAsync(httpContext.HttpContext.User).Result;

            if (user == null && signInManager.IsSignedIn(httpContext.HttpContext.User))
            {
                //Edge case where the user has an invalid session
                //The signin manager thinks the user is logged in, but the token is invalid.
                //This results in the signin manager saying the user is logged in, but the userManager returns null when requesting the current user.
                signInManager.SignOutAsync().Wait();
                httpContext.HttpContext.Response.Redirect(httpContext.HttpContext.Request.GetDisplayUrl()); 
            }
            
            return user;
        }

        public string GetCurrentlyLoggedInUsername()
        {
            return GetCurrentlyLoggedInUser()?.GetDisplayName();
        }
    }
}