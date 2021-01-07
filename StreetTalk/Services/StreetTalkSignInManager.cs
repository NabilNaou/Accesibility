using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StreetTalk.Models;

namespace StreetTalk.Services
{
    public class StreetTalkSignInManager : SignInManager<StreetTalkUser>
    {
        private readonly IEmailSender sender;
        
        public StreetTalkSignInManager(IEmailSender sender, UserManager<StreetTalkUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<StreetTalkUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<StreetTalkUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<StreetTalkUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            this.sender = sender;
        }

        protected override async Task<SignInResult> LockedOut(StreetTalkUser user)
        {
            await sender.SendEmailAsync(user.Email, "Verdachte activiteit", "Uw account is tijdelijk geblokkeerd.");
            return await base.LockedOut(user);
        }
    }
}