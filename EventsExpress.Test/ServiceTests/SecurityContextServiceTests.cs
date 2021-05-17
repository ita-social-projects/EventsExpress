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
        public void GetCurrentUserId_Ok()
        {
            mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(GetClaimsPrincipal());

            var res = securityContext.GetCurrentUserId();

            Assert.That(res, Is.EqualTo(userId));
        }

        [Test]
        public void GetCurrentAccountId_Ok()
        {
            mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(GetClaimsPrincipal());

            var res = securityContext.GetCurrentAccountId();

            Assert.That(res, Is.EqualTo(accountId));
        }

        private ClaimsPrincipal GetClaimsPrincipal()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{userId}"),
                new Claim(ClaimTypes.Sid, $"{accountId}"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            return new ClaimsPrincipal(identity);
        }
    }
}
