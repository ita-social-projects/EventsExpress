using System.Net.Sockets;
using EventsExpress.Core.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    public class IpProviderServiceTests
    {
        private IpProviderService service;
        private Mock<IHttpContextAccessor> httpContextAccessor;

        [SetUp]
        public void Initialize()
        {
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            service = new IpProviderService(httpContextAccessor.Object);
        }

        [Test]
        public void GetIpAddress_ReturnsIpAddressFromHeaders()
        {
            httpContextAccessor.Setup(opt => opt.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")).Returns(true);
            httpContextAccessor.Setup(opt => opt.HttpContext.Request.Headers["X-Forwarded-For"]).Returns("IpAddress");
            var result = service.GetIpAdress();
            Assert.That(result == "IpAddress");
        }

        [Test]
        public void GetIpAddress_ReturnsIpAddressFromRemote()
        {
            httpContextAccessor.Setup(opt => opt.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For")).Returns(false);
            httpContextAccessor.Setup(opt => opt.HttpContext.Connection.RemoteIpAddress).Returns(System.Net.IPAddress.IPv6Any);
            var result = service.GetIpAdress();
            Assert.IsInstanceOf<string>(result);
        }
    }
}
