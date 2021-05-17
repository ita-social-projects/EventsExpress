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
    internal class AuthenticationControllerTests
    {
        private static readonly string JwtToken = "jwtToken";
        private static readonly string RefreshToken = "refreshToken";
        private static readonly string SomeEmail = "someEmail @gmail.com";

        private AuthenticationController _authenticationController;
        private Mock<IAuthService> _authService;
        private Mock<IMapper> _mapper;
        private Mock<ITokenService> _tokenService;
        private Mock<IAccountService> _accountService;
        private Mock<IGoogleSignatureVerificator> _googleSignatureVerificator;

        [SetUp]
        public void Initialize()
        {
            _authService = new Mock<IAuthService>();
            _accountService = new Mock<IAccountService>();
            _mapper = new Mock<IMapper>();
            _tokenService = new Mock<ITokenService>();
            _googleSignatureVerificator = new Mock<IGoogleSignatureVerificator>();
            _authenticationController = new AuthenticationController(
                _mapper.Object,
                _authService.Object,
                _tokenService.Object,
                _accountService.Object,
                _googleSignatureVerificator.Object);
        }

        [Test]
        [Category("Login")]
        public void Login_IncorrectEmailOrPassword_ThrowException()
        {
            _authService.Setup(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.Login(new LoginViewModel()));
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("Login")]
        public async Task Login_CorrectReqest_DoesNotThrowExceptionAsync()
        {
            var loginReqest = new LoginViewModel
            {
                Email = SomeEmail,
                Password = "somePassword",
            };

            _authService.Setup(s => s.Authenticate(loginReqest.Email, loginReqest.Password))
                .ReturnsAsync(new AuthenticateResponseModel(JwtToken, RefreshToken));

            _tokenService.Setup(s => s.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.Login(loginReqest);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("FacebookLogin")]
        public void FacebookLogin_CantAuthenticate_ThrowException()
        {
            _accountService.Setup(s => s.EnsureExternalAccountAsync(It.IsAny<string>(), It.IsAny<AuthExternalType>()));
            _authService.Setup(s => s.Authenticate(It.IsAny<string>(), It.IsAny<AuthExternalType>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.FacebookLogin(new AccountViewModel()));
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("FacebookLogin")]
        public async Task FacebookLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var fb = AuthExternalType.Facebook;
            var accountWiew = new AccountViewModel
            {
                Email = SomeEmail,
            };

            _accountService.Setup(s =>
                s.EnsureExternalAccountAsync(accountWiew.Email, fb));
            _authService.Setup(s =>
                s.Authenticate(accountWiew.Email, fb))
                .ReturnsAsync(new AuthenticateResponseModel(JwtToken, RefreshToken));
            _tokenService.Setup(s => s.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.FacebookLogin(accountWiew);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("GoogleLogin")]
        public void GoogleLogin_CantAuthenticate_ThrowException()
        {
            _googleSignatureVerificator.Setup(s =>
                s.Verify(It.IsAny<string>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.GoogleLogin(new AccountViewModel()));
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("GoogleLogin")]
        public async Task GoogleLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var g = AuthExternalType.Google;
            var accountWiew = new AccountViewModel
            {
                Email = SomeEmail,
                TokenId = "googleTockenId",
            };

            var payload = new GoogleJsonWebSignature.Payload { Email = accountWiew.Email };

            _googleSignatureVerificator.Setup(s =>
                s.Verify(accountWiew.TokenId)).ReturnsAsync(payload);
            _accountService.Setup(s =>
                s.EnsureExternalAccountAsync(payload.Email, g));
            _authService.Setup(s =>
                s.Authenticate(payload.Email, g))
                .ReturnsAsync(new AuthenticateResponseModel(JwtToken, RefreshToken));
            _tokenService.Setup(s => s.SetTokenCookie(RefreshToken));

            var res = await _authenticationController.GoogleLogin(accountWiew);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("TwitterLogin")]
        public void TwitterLogin_CantAuthenticate_ThrowException()
        {
            _accountService.Setup(s =>
                s.EnsureExternalAccountAsync(It.IsAny<string>(), It.IsAny<AuthExternalType>()));
            _authService.Setup(s =>
                s.Authenticate(It.IsAny<string>(), It.IsAny<AuthExternalType>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.TwitterLogin(new AccountViewModel()));
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("TwitterLogin")]
        public async Task TwitterLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var fb = AuthExternalType.Twitter;
            var accountWiew = new AccountViewModel
            {
                Email = SomeEmail,
            };

            _accountService.Setup(s =>
                s.EnsureExternalAccountAsync(accountWiew.Email, fb));
            _authService.Setup(s =>
                s.Authenticate(accountWiew.Email, fb))
                .ReturnsAsync(new AuthenticateResponseModel(JwtToken, RefreshToken));
            _tokenService.Setup(s => s.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.TwitterLogin(accountWiew);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("RegisterBegin")]
        public void RegisterBegin_EmailAlreadyInUse_ThrowException()
        {
            _authService.Setup(s =>
                s.CanRegister(It.IsAny<string>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.RegisterBegin(new LoginViewModel()));
            _authService.Verify(s => s.Register(It.IsAny<RegisterDto>()), Times.Never);
        }

        [Test]
        [Category("RegisterBegin")]
        public async Task RegisterBegin_AllOk_DoesNotThrowExceptionAsync()
        {
            var loginReqest = new LoginViewModel
            {
                Email = SomeEmail,
                Password = "somePassword",
            };

            var registerDto = new RegisterDto
            {
                Email = loginReqest.Email,
                Password = loginReqest.Password,
            };

            _authService.Setup(s =>
                s.CanRegister(loginReqest.Email)).ReturnsAsync(true);
            _mapper.Setup(s =>
                s.Map<RegisterDto>(loginReqest)).Returns(registerDto);
            _authService.Setup(s =>
                s.Register(registerDto)).ReturnsAsync(Guid.NewGuid());

            var res = await _authenticationController.RegisterBegin(loginReqest);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _authService.Verify(s => s.Register(It.IsAny<RegisterDto>()), Times.Once);
        }

        [Test]
        [Category("RegisterComplete")]
        public void RegisterComplete_CantMap_ThrowException()
        {
            _mapper.Setup(s =>
                s.Map<RegisterCompleteDto>(It.IsAny<RegisterCompleteViewModel>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
            _authenticationController.RegisterComplete(new RegisterCompleteViewModel()));
            _authService.Verify(s => s.RegisterComplete(It.IsAny<RegisterCompleteDto>()), Times.Never);
        }

        [Test]
        [Category("RegisterComplete")]
        public async Task RegisterComplete_AllOk_DoesNotThrowExceptionAsync()
        {
            _authenticationController.ControllerContext = new ControllerContext();
            _authenticationController.ControllerContext.HttpContext = new DefaultHttpContext();
            var authRequest = new RegisterCompleteViewModel
            {
                AccountId = Guid.NewGuid(),
            };

            var profileData = new RegisterCompleteDto
            {
                AccountId = authRequest.AccountId,
            };

            var authenticateResponseModel = new AuthenticateResponseModel(JwtToken, RefreshToken);

            _mapper.Setup(s =>
                s.Map<RegisterCompleteDto>(authRequest)).Returns(profileData);
            _authService.Setup(s =>
                s.RegisterComplete(profileData)).Returns(Task.CompletedTask);
            _tokenService.Setup(s =>
                s.RefreshToken(It.IsAny<string>())).ReturnsAsync(authenticateResponseModel);

            var res = await _authenticationController.RegisterComplete(authRequest);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _authService.Verify(s => s.RegisterComplete(It.IsAny<RegisterCompleteDto>()), Times.Once);
            _tokenService.Verify(s => s.RefreshToken(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("PasswordRecovery")]
        public void PasswordRecovery_InvalidEmail_ThrowException()
        {
            AsyncTestDelegate methodInvoke = async () =>
                await _authenticationController.PasswordRecovery(null);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Incorrect email"));
            _authService.Verify(s => s.PasswordRecover(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("PasswordRecovery")]
        public void PasswordRecovery_InvalidAuthLocalId_ThrowException()
        {
            _authService.Setup(s => s.PasswordRecover(It.IsAny<string>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.PasswordRecovery("Incorrect Email"));
        }

        [Test]
        [Category("PasswordRecovery")]
        public async Task PasswordRecovery_AllOk_DoesNotThrowExceptionAsync()
        {
            var res = await _authenticationController.PasswordRecovery(SomeEmail);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _authService.Verify(s => s.PasswordRecover(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("EmailConfirm")]
        public void EmailConfirm_InvalidAuthLocalId_ThrowException()
        {
            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.EmailConfirm("InvalidAuthLocalId", "Token"));
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("EmailConfirm")]
        public void EmailConfirm_ConfirmationFailed_ThrowException()
        {
            _authService.Setup(s =>
                s.EmailConfirmAndAuthenticate(It.IsAny<Guid>(), It.IsAny<string>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.EmailConfirm(Guid.NewGuid().ToString(), "Token"));
            _tokenService.Verify(s => s.SetTokenCookie(It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("EmailConfirm")]
        public async Task EmailConfirm_AllOk_DoesNotThrowExceptionAsync()
        {
            _authService.Setup(s =>
                s.EmailConfirmAndAuthenticate(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new AuthenticateResponseModel(JwtToken, RefreshToken));
            _tokenService.Setup(s => s.SetTokenCookie(It.IsAny<string>()));

            var res = await _authenticationController.EmailConfirm(Guid.NewGuid().ToString(), "Token");

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _tokenService.Verify(a => a.SetTokenCookie(It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("ChangePassword")]
        public void ChangePassword_InvalidAuthLocalId_ThrowException()
        {
            _authenticationController.ControllerContext = new ControllerContext();
            _authenticationController.ControllerContext.HttpContext = new DefaultHttpContext();
            _authService.Setup(s =>
            s.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _authenticationController.ChangePassword(new ChangeViewModel()));
        }

        [Test]
        [Category("ChangePassword")]
        public async Task ChangePassword_AllOk_DoesNotThrowExceptionAsync()
        {
            _authenticationController.ControllerContext = new ControllerContext();
            _authenticationController.ControllerContext.HttpContext = new DefaultHttpContext();
            var model = new ChangeViewModel
            {
                OldPassword = "old password",
                NewPassword = "new password",
            };

            var res = await _authenticationController.ChangePassword(model);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _authService.Verify(s => s.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
