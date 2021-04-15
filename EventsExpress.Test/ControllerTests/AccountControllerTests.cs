using System;
using System.Collections.Generic;
using System.Security.Claims;
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
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class AccountControllerTests
    {
        private static readonly string SomeEmail = "someEmail@gmail.com";

        private AccountController _accountController;
        private Mock<IAuthService> _authService;
        private Mock<IMapper> _mapper;
        private Mock<IAccountService> _accountService;
        private Mock<IGoogleSignatureVerificator> _googleSignatureVerificator;

        [SetUp]
        public void Initialize()
        {
            _mapper = new Mock<IMapper>();
            _authService = new Mock<IAuthService>();
            _accountService = new Mock<IAccountService>();
            _googleSignatureVerificator = new Mock<IGoogleSignatureVerificator>();
            _accountController = new AccountController(
                _mapper.Object,
                _authService.Object,
                _accountService.Object,
                _googleSignatureVerificator.Object);
            _accountController.ControllerContext = new ControllerContext();
            _accountController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        [Category("GetLinkedAuth")]
        public void GetLinkedAuth_InvalidUser_ThrowException()
        {
            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _accountController.GetLinkedAuth());
        }

        [Test]
        [Category("GetLinkedAuth")]
        public async Task GetLinkedAuth_AllOk_DoesNotThrowExceptionAsync()
        {
            var user = new UserDto
            {
                AccountId = Guid.NewGuid(),
            };

            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _accountService.Setup(s =>
                s.GetLinkedAuth(user.AccountId)).ReturnsAsync(new List<AuthDto>());
            _mapper.Setup(s =>
                s.Map<IEnumerable<AuthViewModel>>(It.IsAny<IEnumerable<AuthDto>>())).Returns(new List<AuthViewModel>());

            var res = await _accountController.GetLinkedAuth();

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _accountService.Verify(s => s.GetLinkedAuth(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        [Category("AddGoogleLogin")]
        public void AddGoogleLogin_InvalidGoogleSignature_ThrowException()
        {
            _googleSignatureVerificator.Setup(s =>
                s.Verify(It.IsAny<string>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _accountController.AddGoogleLogin(new AuthGoogleViewModel()));
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<AuthExternalType>()), Times.Never);
        }

        [Test]
        [Category("AddGoogleLogin")]
        public async Task AddGoogleLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var model = new AuthGoogleViewModel
            {
                Email = SomeEmail,
                TokenId = "tokenId",
            };
            var user = new UserDto
            {
                AccountId = Guid.NewGuid(),
            };

            _googleSignatureVerificator.Setup(s =>
                s.Verify(model.TokenId)).ReturnsAsync(new GoogleJsonWebSignature.Payload());
            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _accountService.Setup(s =>
                s.AddAuth(user.AccountId, model.Email, AuthExternalType.Google)).Returns(Task.CompletedTask);

            var res = await _accountController.AddGoogleLogin(model);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), AuthExternalType.Google), Times.Once);
        }

        [Test]
        [Category("AddFacebookLogin")]
        public void AddFacebookLogin_InvalidUser_ThrowException()
        {
            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _accountController.AddFacebookLogin(new AuthExternalViewModel()));
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<AuthExternalType>()), Times.Never);
        }

        [Test]
        [Category("AddFacebookLogin")]
        public async Task AddFacebookLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var model = new AuthExternalViewModel
            {
                Email = SomeEmail,
            };
            var user = new UserDto
            {
                AccountId = Guid.NewGuid(),
            };

            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _accountService.Setup(s =>
                s.AddAuth(user.AccountId, model.Email, AuthExternalType.Facebook)).Returns(Task.CompletedTask);

            var res = await _accountController.AddFacebookLogin(model);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), AuthExternalType.Facebook), Times.Once);
        }

        [Test]
        [Category("AddTwitterLogin")]
        public void AddTwitterLogin_InvalidUser_ThrowException()
        {
            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _accountController.AddTwitterLogin(new AuthExternalViewModel()));
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<AuthExternalType>()), Times.Never);
        }

        [Test]
        [Category("AddTwitterLogin")]
        public async Task AddTwitterLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var model = new AuthExternalViewModel
            {
                Email = SomeEmail,
            };
            var user = new UserDto
            {
                AccountId = Guid.NewGuid(),
            };

            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _accountService.Setup(s =>
                s.AddAuth(user.AccountId, model.Email, AuthExternalType.Twitter)).Returns(Task.CompletedTask);

            var res = await _accountController.AddTwitterLogin(model);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), AuthExternalType.Twitter), Times.Once);
        }

        [Test]
        [Category("AddLocalLogin")]
        public void AddLocalLogin_InvalidUser_ThrowException()
        {
            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                _accountController.AddLocalLogin(new LoginViewModel()));
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        [Category("AddLocalLogin")]
        public async Task AddLocalLogin_AllOk_DoesNotThrowExceptionAsync()
        {
            var model = new LoginViewModel
            {
                Email = SomeEmail,
                Password = "SomePassword",
            };
            var user = new UserDto
            {
                AccountId = Guid.NewGuid(),
            };

            _authService.Setup(s =>
                s.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _accountService.Setup(s =>
                s.AddAuth(user.AccountId, model.Email, model.Password)).Returns(Task.CompletedTask);

            var res = await _accountController.AddLocalLogin(model);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(
                s => s.AddAuth(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        [Category("Block")]
        public void Block_User_ThrowException()
        {
            Guid userId = Guid.NewGuid();
            _accountService.Setup(service => service.Block(It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _accountController.Block(userId));
            _accountService.Verify(service => service.Block(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        [Category("Block")]
        public async Task Block_CorrectIdUser_OkResultAsync()
        {
            Guid userId = Guid.NewGuid();
            _accountService.Setup(service => service.Block(userId));

            var res = await _accountController.Block(userId);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(service => service.Block(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        [Category("UnBlock")]
        public void UnBlock_CorrectIdUser_ThrowException()
        {
            Guid userId = Guid.NewGuid();
            _accountService.Setup(service => service.Unblock(It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _accountController.Unblock(userId));
            _accountService.Verify(service => service.Unblock(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        [Category("UnBlock")]
        public async Task UnBlock_CorrectIdUser_OkResultAsync()
        {
            Guid userId = Guid.NewGuid();
            _accountService.Setup(service => service.Unblock(userId));

            var res = await _accountController.Unblock(userId);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(service => service.Unblock(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        [Category("ChangeRoles")]
        public void ChangeRoles_InvalidModel_ThrowException()
        {
            _mapper.Setup(s =>
                s.Map<IEnumerable<Db.Entities.Role>>(It.IsAny<IEnumerable<RoleViewModel>>()))
                .Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _accountController.ChangeRoles(new ChangeRoleWiewModel()));
            _accountService.Verify(
                s => s.ChangeRole(It.IsAny<Guid>(), It.IsAny<IEnumerable<Db.Entities.Role>>()), Times.Never);
        }

        [Test]
        [Category("ChangeRoles")]
        public async Task ChangeRoles_IdUser_OkResultAsync()
        {
            _mapper.Setup(s =>
                s.Map<IEnumerable<Db.Entities.Role>>(It.IsAny<IEnumerable<RoleViewModel>>()))
                .Returns(new List<Db.Entities.Role>());
            _accountService.Setup(s =>
                s.ChangeRole(It.IsAny<Guid>(), It.IsAny<IEnumerable<Db.Entities.Role>>())).Returns(Task.CompletedTask);

            var res = await _accountController.ChangeRoles(new ChangeRoleWiewModel());

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _accountService.Verify(
                s => s.ChangeRole(It.IsAny<Guid>(), It.IsAny<IEnumerable<Db.Entities.Role>>()), Times.Once);
        }
    }
}
