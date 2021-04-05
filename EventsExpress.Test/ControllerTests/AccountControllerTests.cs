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
    internal class AccountControllerTests
    {
        private AccountController _accountController;
        private Mock<IAuthService> _authService;
        private Mock<IMapper> _mapper;
        private Mock<IAccountService> _accountService;

        [SetUp]
        public void Initialize()
        {
            _authService = new Mock<IAuthService>();
            _accountService = new Mock<IAccountService>();
            _mapper = new Mock<IMapper>();
            _accountController = new AccountController(_mapper.Object, _authService.Object, _accountService.Object);
        }

        [Test]
        [Category("Block")]
        public void Block_User_ThrowExceptionAsync()
        {
            Guid userId = Guid.NewGuid();
            _accountService.Setup(service => service.Block(It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _accountController.Block(userId));
            _accountService.Verify(service => service.Block(It.IsAny<Guid>()), Times.Exactly(1));
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
            _accountService.Verify(service => service.Block(It.IsAny<Guid>()), Times.Exactly(1));
        }

        [Test]
        [Category("UnBlock")]
        public void UnBlock_CorrectIdUser_ThrowException()
        {
            Guid userId = Guid.NewGuid();
            _accountService.Setup(service => service.Unblock(It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _accountController.Unblock(userId));
            _accountService.Verify(service => service.Unblock(It.IsAny<Guid>()), Times.Exactly(1));
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
            _accountService.Verify(service => service.Unblock(It.IsAny<Guid>()), Times.Exactly(1));
        }

        [Test]
        [Category("ChangeRole")]
        public void ChangeRole_IdUser_ThrowException()
        {
            /* _userService.Setup(user => user.ChangeRole(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _usersController.ChangeRole(It.IsAny<Guid>(), It.IsAny<Guid>()));
            _userService.Verify(us => us.ChangeRole(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Exactly(1)); */
        }

        [Test]
        [Category("ChangeRole")]
        public void ChangeRole_IdUser_OkResultAsync()
        {
           /*  Guid idUser = _idUser;
            Guid idRole = _idRole;
            _userService.Setup(user => user.ChangeRole(idUser, idRole));

            var res = await _usersController.ChangeRole(idUser, idRole);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.ChangeRole(idUser, idRole), Times.Exactly(1)); */
        }
    }
}
