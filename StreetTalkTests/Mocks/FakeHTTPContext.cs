using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Moq;

namespace StreetTalkTests.Mocks
{
    public class FakeHttpContext : IHttpContextAccessor
    {
        public HttpContext HttpContext
        {
            get
            {
                var context = new Mock<HttpContext>();
                var request = new Mock<HttpRequest>();
                var response = new Mock<HttpResponse>();
                var session = new Mock<ISession>();
                var user = new Mock<ClaimsPrincipal>();
                var identity = new Mock<IIdentity>();

                context.Setup(c=> c.Request).Returns(request.Object);
                context.Setup(c=> c.Response).Returns(response.Object);
                context.Setup(c=> c.Session).Returns(session.Object);
                context.Setup(c=> c.User).Returns(user.Object);
                user.Setup(c=> c.Identity).Returns(identity.Object);
                identity.Setup(i => i.IsAuthenticated).Returns(true);
                identity.Setup(i => i.Name).Returns("moderator@streettalk.nl");

                return context.Object;
            }
            set { }
        }
    }
}