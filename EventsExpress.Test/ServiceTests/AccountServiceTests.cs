using System;
using System.Security.Claims;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;
using EventsExpress.Test.ServiceTests.TestClasses.Auth;
using MediatR;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class AccountServiceTests : TestInitializer
    {
        private static Guid existingUserId = Guid.NewGuid();
        private Account existingAccount;

        private Mock<IMediator> mockMediator;
        private AccountService service;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            service = new AccountService(
                Context,
                mockMediator.Object);

            existingAccount = new Account { UserId = existingUserId };

            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();
        }

        [Test]
        public void Block_InvalidUser_ThrowException()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Block(Guid.NewGuid()));
        }

        [Test]
        public void Block_AllIsValid_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await service.Block(existingUserId));
        }

        [Test]
        public void Unblock_InvalidUser_ThrowException()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Unblock(Guid.NewGuid()));
        }

        [Test]
        public void Unblock_AllIsValid_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await service.Unblock(existingUserId));
        }

        /*[Test]
        public void ChangeRole_InvalidRole_ReturnFalse()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangeRole(userId, roleId));
        }

        [Test]
        public void ChangeRole_InvalidUser_ReturnFalse()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangeRole(userId, roleId));
        }

        [Test]
        public void ChangeRole_RoleIdAndUserIdIsValid_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.ChangeRole(userId, roleId));
        } */
    }
}
