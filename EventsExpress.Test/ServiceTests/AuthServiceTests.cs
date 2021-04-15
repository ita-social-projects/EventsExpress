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
    internal class AuthServiceTests : TestInitializer
    {
        private static readonly Guid AuthLocalId = ConfirmEmail.AuthLocalId;
        private readonly string name = "existingName";
        private readonly string existingEmail = "existingEmail@gmail.com";

        private Mock<IUserService> mockUserService;
        private Mock<ITokenService> mockTokenService;
        private Mock<ICacheHelper> mockCacheHelper;
        private Mock<IEmailService> mockEmailService;
        private Mock<IMediator> mockMediator;
        private AuthService service;

        private UserDto existingUserDTO;
        private User existingUser;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockUserService = new Mock<IUserService>();
            mockTokenService = new Mock<ITokenService>();
            mockCacheHelper = new Mock<ICacheHelper>();
            mockEmailService = new Mock<IEmailService>();
            mockMediator = new Mock<IMediator>();
            service = new AuthService(
                Context,
                MockMapper.Object,
                mockUserService.Object,
                mockTokenService.Object,
                mockCacheHelper.Object,
                mockEmailService.Object,
                mockMediator.Object);

            existingUser = new User
            {
                Id = AuthLocalId,
                Name = name,
                Email = existingEmail,
            };

            existingUserDTO = new UserDto
            {
                Id = AuthLocalId,
                Name = name,
                Email = existingEmail,
            };

            Context.Users.Add(existingUser);
            Context.SaveChanges();
        }

        [Test]
        public void ChangePasswordAsync_InvalidUserClaims_Throws()
        {
            var userDtoWithoutAuthLocal = new UserDto
            {
                Account = new Account(),
            };

            mockUserService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns(userDtoWithoutAuthLocal);

            AsyncTestDelegate methodInvoke = async () =>
                await service.ChangePasswordAsync(GetClaimsPrincipal(), "validPassword", "newPassword");

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Invalid user"));
        }

        [Test]
        public void ChangePasswordAsync_InvalidPassword_Throws()
        {
            string salt = PasswordHasher.GenerateSalt();
            string validPassword = "validPassword";
            string invalidPassword = "invalidPassword";
            UserDto userDto = new UserDto
            {
                Account = new Account
                {
                    AuthLocal = new AuthLocal
                    {
                        Salt = salt,
                        PasswordHash = PasswordHasher.GenerateHash(validPassword, salt),
                    },
                },
            };

            mockUserService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns(userDto);

            AsyncTestDelegate methodInvoke = async () =>
                await service.ChangePasswordAsync(GetClaimsPrincipal(), invalidPassword, "newPassword");

            Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
        }

        [Test]
        public void ChangePasswordAsync_ValidPassword_DoesNotThrows()
        {
            string salt = PasswordHasher.GenerateSalt();
            string validPassword = "validPassword";
            UserDto userDto = new UserDto
            {
                Account = new Account
                {
                    AuthLocal = new AuthLocal
                    {
                        Salt = salt,
                        PasswordHash = PasswordHasher.GenerateHash(validPassword, salt),
                    },
                },
            };

            mockUserService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns(userDto);

            AsyncTestDelegate methodInvoke = async () =>
                await service.ChangePasswordAsync(GetClaimsPrincipal(), validPassword, "newPassword");

            Assert.DoesNotThrowAsync(methodInvoke);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConfirmEmail_TokenIsNullOrEmpty_ReturnFalse(string token)
        {
            CacheDto cache = new CacheDto()
            {
                AuthLocalId = existingUser.Id,
                Token = token,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.EmailConfirmAndAuthenticate(cache.AuthLocalId, token));
        }

        [Test]
        public void ConfirmEmail_ValidCacheDto_ReturnTrue()
        {
            CacheDto cache = new CacheDto()
            {
                AuthLocalId = existingUser.Id,
                Token = "validToken",
            };

            var authLocal = new AuthLocal
            {
                Id = AuthLocalId,
                Account = new Account(),
            };

            Context.AuthLocal.Add(authLocal);
            Context.SaveChanges();

            mockCacheHelper.Setup(ch => ch.GetValue(cache.AuthLocalId))
                .Returns(new CacheDto { Token = cache.Token });
            mockTokenService.Setup(ts => ts.GenerateAccessToken(It.IsAny<Account>()))
                .Returns("AccessToken");
            mockTokenService.Setup(ts => ts.GenerateRefreshToken())
                .Returns(new RefreshToken());

            AsyncTestDelegate methodInvoke = async () =>
                await service.EmailConfirmAndAuthenticate(cache.AuthLocalId, cache.Token);
            Assert.DoesNotThrowAsync(methodInvoke);
        }

        [Test]
        [TestCaseSource(typeof(ConfirmEmail), nameof(ConfirmEmail.TestCases))]
        public void ConfirmEmail_CachingFailed_Throws(Guid id, string token)
        {
            mockCacheHelper.Setup(u => u.GetValue(It.IsAny<Guid>()));

            AsyncTestDelegate methodInvoke = async () =>
                await service.EmailConfirmAndAuthenticate(id, token);
            Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
        }

        [Test]
        public void PasswordRecovery_UserNoFoundInDb_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.PasswordRecover(new UserDto().Email));
        }

        [Test]
        public void PasswordRecovery_ValidUserDto_ReturnTrue()
        {
            var authLocal = new AuthLocal
            {
                Id = AuthLocalId,
                Account = new Account(),
                Email = existingUserDTO.Email,
            };

            Context.AuthLocal.Add(authLocal);
            Context.SaveChanges();

            Assert.DoesNotThrowAsync(async () => await service.PasswordRecover(existingUserDTO.Email));
        }

        private ClaimsPrincipal GetClaimsPrincipal()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{Guid.NewGuid()}"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            return new ClaimsPrincipal(identity);
        }
    }
}
