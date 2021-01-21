using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StreetTalk.Models;

namespace StreetTalkTests.Mocks
{
    public class FakeSignInManager : SignInManager<StreetTalkUser>
    {
        public FakeSignInManager()
            : base(new FakeUserManager(),
                new FakeHttpContext(),
                new Mock<IUserClaimsPrincipalFactory<StreetTalkUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<StreetTalkUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<StreetTalkUser>>().Object)
        { }

        public override async Task SignOutAsync()
        {
            
        }
    }
}