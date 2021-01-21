using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StreetTalk.Models;

namespace StreetTalkTests.Mocks
{
    public class FakeUserManager : UserManager<StreetTalkUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<StreetTalkUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<StreetTalkUser>>().Object,
                new IUserValidator<StreetTalkUser>[0],
                new IPasswordValidator<StreetTalkUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<StreetTalkUser>>>().Object)
        { }

        public override Task<IdentityResult> CreateAsync(StreetTalkUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> AddToRoleAsync(StreetTalkUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(StreetTalkUser user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}