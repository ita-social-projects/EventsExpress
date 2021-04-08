using System;
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
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class AuthenticationControllerTests
    {
        private AuthenticationController _authenticationController;
        private Mock<IAuthService> _authService;
        private Mock<IMapper> _mapper;
        private Mock<ITokenService> _tokenService;
        private Mock<IAccountService> _accountService;

        [SetUp]
        public void Initialize()
        {
            _authService = new Mock<IAuthService>();
            _accountService = new Mock<IAccountService>();
            _mapper = new Mock<IMapper>();
            _tokenService = new Mock<ITokenService>();
            _authenticationController = new AuthenticationController(
                _mapper.Object,
                _authService.Object,
                _tokenService.Object,
                _accountService.Object);
        }

        [Test]
        [Category("Login")]
        public void Login_IncorrectEmailOrPassword_ThrowException()
        {
            _authService.Setup(service =>
                service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.Login(new LoginViewModel { }));
            _tokenService.Verify(service => service.SetTokenCookie(It.IsAny<string>()), Times.Never());
        }

        [Test]
        [Category("Login")]
        public async Task Login_Login_CorrectReqest_DoesNotThrowExceptionAsync()
        {
            var loginReqest = new LoginViewModel
            {
                Email = "someEmail@gmail.com",
                Password = "somePassword",
            };

            _authService.Setup(service =>
                service.Authenticate(loginReqest.Email, loginReqest.Password))
                .ReturnsAsync(new AuthenticateResponseModel("jwtToken", "refreshToken"));

            _tokenService.Setup(service => service.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.Login(loginReqest);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(service => service.SetTokenCookie(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("FacebookLogin")]
        public void FacebookLogin_CantAuthenticate_ThrowException()
        {
            _accountService.Setup(service =>
                service.EnsureExternalAccountAsync(It.IsAny<string>(), It.IsAny<AuthExternalType>()));
            _authService.Setup(service =>
                service.Authenticate(It.IsAny<string>(), It.IsAny<AuthExternalType>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.FacebookLogin(new AccountViewModel { }));
            _tokenService.Verify(service => service.SetTokenCookie(It.IsAny<string>()), Times.Never());
        }

        [Test]
        [Category("FacebookLogin")]
        public async Task FacebookLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var fb = AuthExternalType.Facebook;
            var accountWiew = new AccountViewModel
            {
                Email = "someEmail@gmail.com",
            };

            _accountService.Setup(service =>
                service.EnsureExternalAccountAsync(accountWiew.Email, fb));
            _authService.Setup(service =>
                service.Authenticate(accountWiew.Email, fb))
                .ReturnsAsync(new AuthenticateResponseModel("jwtToken", "refreshToken"));
            _tokenService.Setup(service => service.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.FacebookLogin(accountWiew);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(service => service.SetTokenCookie(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("TwitterLogin")]
        public void TwitterLogin_CantAuthenticate_ThrowException()
        {
            _accountService.Setup(service =>
                service.EnsureExternalAccountAsync(It.IsAny<string>(), It.IsAny<AuthExternalType>()));
            _authService.Setup(service =>
                service.Authenticate(It.IsAny<string>(), It.IsAny<AuthExternalType>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.TwitterLogin(new AccountViewModel { }));
            _tokenService.Verify(service => service.SetTokenCookie(It.IsAny<string>()), Times.Never());
        }

        [Test]
        [Category("TwitterLogin")]
        public async Task TwitterLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var fb = AuthExternalType.Twitter;
            var accountWiew = new AccountViewModel
            {
                Email = "someEmail@gmail.com",
            };

            _accountService.Setup(service =>
                service.EnsureExternalAccountAsync(accountWiew.Email, fb));
            _authService.Setup(service =>
                service.Authenticate(accountWiew.Email, fb))
                .ReturnsAsync(new AuthenticateResponseModel("jwtToken", "refreshToken"));
            _tokenService.Setup(service => service.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.TwitterLogin(accountWiew);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(service => service.SetTokenCookie(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("RegisterBegin")]
        public void RegisterBegin_EmailAlreadyInUse_ThrowException()
        {
            _authService.Setup(service =>
                service.CanRegister(It.IsAny<string>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.RegisterBegin(new LoginViewModel { }));
            _authService.Verify(service => service.Register(It.IsAny<RegisterDto>()), Times.Never);
        }

        [Test]
        [Category("RegisterBegin")]
        public async Task RegisterBegin_AllOk_DoesNotThrowExceptionAsync()
        {
            var loginReqest = new LoginViewModel
            {
                Email = "someEmail@gmail.com",
                Password = "somePassword",
            };
            var registerDto = new RegisterDto
            {
                Email = loginReqest.Email,
                Password = loginReqest.Password,
            };

            _authService.Setup(service =>
                service.CanRegister(loginReqest.Email)).ReturnsAsync(true);
            _mapper.Setup(service =>
                service.Map<RegisterDto>(loginReqest)).Returns(registerDto);
            _authService.Setup(service =>
                service.Register(registerDto)).ReturnsAsync(Guid.NewGuid());

            var res = await _authenticationController.RegisterBegin(loginReqest);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _authService.Verify(service => service.Register(It.IsAny<RegisterDto>()), Times.Exactly(1));
        }

        [Test]
        [Category("RegisterComplete")]
        public void RegisterComplete_CantMap_ThrowException()
        {
            _mapper.Setup(service =>
                service.Map<RegisterCompleteDto>(It.IsAny<RegisterCompleteViewModel>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
            _authenticationController.RegisterComplete(new RegisterCompleteViewModel { }));
            _authService.Verify(service => service.RegisterComplete(It.IsAny<RegisterCompleteDto>()), Times.Never());
        }

        /* [Test]
        [Category("RegisterComplete")]
        public async Task RegisterComplete_AllOk_DoesNotThrowExceptionAsync()
        {
            var authRequest = new RegisterCompleteViewModel
            {
                AccountId = Guid.NewGuid(),
            };

        }*/
    }
}
