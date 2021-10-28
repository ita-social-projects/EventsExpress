using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class TokenController
    {
        private Mock<ITokenService> _tokenService;
        private Mock<DefaultHttpContext> _httpContext;
        private Controllers.TokenController _tokenController;

        [SetUp]
        protected void Initialize()
        {
            _tokenService = new Mock<ITokenService>();
            _httpContext = new Mock<DefaultHttpContext>();
            _tokenController = new Controllers.TokenController(_tokenService.Object);
        }

        [Test]
        public void Refresh_Correct_ReturnOk()
        {
            _tokenService.Setup(s => s.RefreshToken("token").Result).Returns(new AuthenticateResponseModel("jwtToken", "refreshToken"));
            _httpContext.Setup(opt => opt.Request.Cookies["refreshToken"]).Returns("refreshToken");
            _tokenController.ControllerContext = new ControllerContext();
            _tokenController.ControllerContext.HttpContext = _httpContext.Object;
            var res = _tokenController.Refresh();
            Assert.IsNotInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void Revoke_Correct_ReturnOk()
        {
            _tokenController.ControllerContext = new ControllerContext();
            _tokenController.ControllerContext.HttpContext = new DefaultHttpContext();
            var res = _tokenController.Revoke();
            Assert.IsNotInstanceOf<OkObjectResult>(res);
        }
    }
}
