using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class TokenServiceController
    {
        private TokenController _tokenController;
        private Mock<ITokenService> _tokenService;

        [SetUp]
        public void Initialize()
        {
            _tokenService = new Mock<ITokenService>();
            _tokenController = new TokenController(_tokenService.Object);
        }

        [Test]
        public async Task Refresh_InCorrect_ReturnUnauthorized()
        {
            _tokenController.ControllerContext = new ControllerContext();
            _tokenController.ControllerContext.HttpContext = new DefaultHttpContext();

            var res = await _tokenController.Refresh();

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsNotInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public async Task Revoke_InCorrect_ReturnNotFound()
        {
            _tokenController.ControllerContext = new ControllerContext();
            _tokenController.ControllerContext.HttpContext = new DefaultHttpContext();

            var res = await _tokenController.Revoke();

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsNotInstanceOf<OkObjectResult>(res);
        }
    }
}
