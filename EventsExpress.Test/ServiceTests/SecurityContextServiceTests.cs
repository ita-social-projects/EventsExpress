using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    public class SecurityContextServiceTests : TestInitializer
    {
        private SecurityContextService securityContext;
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;

        private Guid userId = Guid.NewGuid();
        private Guid accountId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            securityContext = new SecurityContextService(mockHttpContextAccessor.Object);
        }

        [Test]
        public void GetCurrentUserId_DoesNotThrows()
        {
            mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(GetClaimsPrincipal());

            TestDelegate methodInvoke = () => securityContext.GetCurrentUserId();

            Assert.DoesNotThrow(methodInvoke);
        }

        [Test]
        public void GetCurrentUserId_Throws()
        {
            mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(GetNullClaimsPrincipal());

            TestDelegate methodInvoke = () => securityContext.GetCurrentUserId();

            var ex = Assert.Throws<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("User not found"));
        }

        private ClaimsPrincipal GetClaimsPrincipal()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{Guid.NewGuid()}"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            return new ClaimsPrincipal(identity);
        }

        private ClaimsPrincipal GetNullClaimsPrincipal()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{null}"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            return new ClaimsPrincipal(identity);
        }
    }
}
