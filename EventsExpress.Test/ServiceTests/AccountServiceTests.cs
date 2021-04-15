using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;
using EventsExpress.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        private Mock<IMediator> _mockMediator;
        private AccountService service;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            _mockMediator = new Mock<IMediator>();
            service = new AccountService(
                Context,
                _mockMediator.Object,
                MockMapper.Object);

            existingAccount = new Account
            {
                Id = Guid.NewGuid(),
                UserId = existingUserId,
                AccountRoles = new[]
                {
                    new AccountRole
                    {
                        RoleId = Db.Enums.Role.User,
                    },
                },
            };

            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();
        }

        [Test]
        [Category("ChangeRole")]
        public void ChangeRole_InvalidUser_ThrowException()
        {
            var userId = Guid.NewGuid();
            List<Role> roles = null;

            AsyncTestDelegate methodInvoke = async () =>
                await service.ChangeRole(userId, roles);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Invalid Roles"));
        }

        [Test]
        [Category("ChangeRole")]
        public void ChangeRole_InvalidRole_ThrowException()
        {
            var userId = Guid.NewGuid();
            var roles = new List<Role>
            {
                new Role
                {
                    Id = Db.Enums.Role.Admin,
                },
            };

            AsyncTestDelegate methodInvoke = async () =>
                await service.ChangeRole(userId, roles);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Invalid user Id"));
        }

        [Test]
        [Category("ChangeRole")]
        public void ChangeRole_AllIsValid_DoesNotThrow()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = Db.Enums.Role.Admin,
                },
            };

            Assert.DoesNotThrowAsync(async () => await service.ChangeRole(existingUserId, roles));
            Assert.NotNull(Context.AccountRoles
                .FirstOrDefault(ar => ar.AccountId == existingAccount.Id
                && ar.RoleId == roles.First().Id));
        }

        [Test]
        [Category("Add External Auth")]
        public void AddAuth_LoginInUse_ThrowException()
        {
            var auth = new AuthExternal
            {
                Email = "someEmail",
                Type = Db.Enums.AuthExternalType.Google,
            };
            Context.AuthExternal.Add(auth);
            Context.SaveChanges();

            AsyncTestDelegate methodInvoke = async () =>
                await service.AddAuth(Guid.NewGuid(), auth.Email, auth.Type);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("This login already in use"));
        }

        [Test]
        [Category("Add External Auth")]
        public void AddAuth_AllIsValid_DoesNotThrow()
        {
            var newId = Guid.NewGuid();
            var newEmail = "someEmail@gmail.com";
            var newType = Db.Enums.AuthExternalType.Facebook;

            Assert.DoesNotThrowAsync(async () => await service.AddAuth(newId, newEmail, newType));
            Assert.True(Context.AuthExternal.Any(ae => ae.AccountId == newId));
        }

        [Test]
        [Category("Add Local Auth")]
        public void AddAuthLocal_LoginInUse_ThrowException()
        {
            var auth = new AuthLocal
            {
                Email = "someEmail",
            };
            Context.AuthLocal.Add(auth);
            Context.SaveChanges();

            AsyncTestDelegate methodInvoke = async () =>
                await service.AddAuth(Guid.NewGuid(), auth.Email, "password");

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("This login already in use"));
        }

        [Test]
        [Category("Add Local Auth")]
        public void AddAuthLocal_AllIsValid_DoesNotThrow()
        {
            var newId = Guid.NewGuid();
            var newEmail = "someEmail@gmail.com";
            var newPassword = "somePassword";

            Assert.DoesNotThrowAsync(async () => await service.AddAuth(newId, newEmail, newPassword));
            Assert.True(Context.AuthLocal.Any(ae => ae.AccountId == newId));
        }

        [Test]
        [Category("EnsureExternalAccount")]
        public void EnsureExternalAccount_AccountExist_DoesNotCreateNewAccount()
        {
            var existAuth = new AuthExternal
            {
                Id = Guid.NewGuid(),
                Email = "someEmail",
                Type = Db.Enums.AuthExternalType.Google,
            };
            Context.AuthExternal.Add(existAuth);
            Context.SaveChanges();

            Assert.DoesNotThrowAsync(async () => await
                service.EnsureExternalAccountAsync(existAuth.Email, existAuth.Type));
            Assert.True(Context.AuthExternal.FirstOrDefault(ae => ae.Id == existAuth.Id).Account == null);
        }

        [Test]
        [Category("EnsureExternalAccount")]
        public void EnsureExternalAccount_AuthNotExist_CreateNewAccount()
        {
            var newAuthType = Db.Enums.AuthExternalType.Google;

            Assert.DoesNotThrowAsync(async () => await
                service.EnsureExternalAccountAsync("someEmail", newAuthType));
            Assert.True(Context.AuthExternal.FirstOrDefault(ae => ae.Email == "someEmail").Account != null);
        }

        [Test]
        [Category("GetLinkedAuth")]
        public void GetLinkedAuth_NullAuthExist_Return()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetLinkedAuth(Guid.NewGuid()));
        }

        [Test]
        [Category("GetLinkedAuth")]
        public async Task GetLinkedAuth_AllOk_ReturnList()
        {
            var existAccountId = Guid.NewGuid();
            var someAccount = new Account
            {
                Id = existAccountId,
                AuthLocal = new AuthLocal
                {
                    Email = "someEmail",
                },
                AuthExternal = new[]
                {
                    new AuthExternal
                    {
                        Type = Db.Enums.AuthExternalType.Google,
                        Email = "someEmail",
                    },
                },
            };
            Context.Accounts.Add(someAccount);
            Context.SaveChanges();

            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountMapperProfile());
            });
            MockMapper.Setup(s => s.ConfigurationProvider).Returns(mockMapperConfig);

            var res = await service.GetLinkedAuth(someAccount.Id);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<IEnumerable<AuthDto>>(res);
        }

        [Test]
        [Category("Block")]
        public void AddAuth_InvalidUser_ThrowException()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Block(Guid.NewGuid()));
        }

        [Test]
        [Category("Block")]
        public void Block_AllIsValid_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await service.Block(existingUserId));
        }

        [Test]
        [Category("Unblock")]
        public void Unblock_InvalidUser_ThrowException()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Unblock(Guid.NewGuid()));
        }

        [Test]
        [Category("Unblock")]
        public void Unblock_AllIsValid_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await service.Unblock(existingUserId));
        }
    }
}
