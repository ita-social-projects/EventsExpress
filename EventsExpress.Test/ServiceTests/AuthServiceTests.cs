using System;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Test.ServiceTests.TestClasses.Auth;
using MediatR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class AuthServiceTests : TestInitializer
    {
        private static readonly Guid AuthLocalId = ConfirmEmail.AuthLocalId;
        private readonly string name = "existingName";
        private readonly string existingEmail = "existingEmail@gmail.com";
        private readonly string validPassword = "validPassword";
        private readonly string invalidPassword = "invalidPassword";

        private Mock<IUserService> mockUserService;
        private Mock<ITokenService> mockTokenService;
        private Mock<ICacheHelper> mockCacheHelper;
        private Mock<IEmailService> mockEmailService;
        private Mock<IPasswordHasher> mockPasswordHasherService;
        private Mock<IMediator> mockMediator;
        private Mock<ISecurityContext> mockSecurityContext;
        private AuthService service;
        private Guid idUser = Guid.NewGuid();
        private Guid idAccount = Guid.NewGuid();

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
            mockPasswordHasherService = new Mock<IPasswordHasher>();
            mockMediator = new Mock<IMediator>();
            mockSecurityContext = new Mock<ISecurityContext>();
            service = new AuthService(
                Context,
                MockMapper.Object,
                mockUserService.Object,
                mockTokenService.Object,
                mockCacheHelper.Object,
                mockEmailService.Object,
                mockMediator.Object,
                mockPasswordHasherService.Object,
                mockSecurityContext.Object);

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

            mockPasswordHasherService.Setup(s => s.GenerateSalt()).Returns("salt");
            mockPasswordHasherService.Setup(s => s.GenerateHash(validPassword, "salt")).Returns("hash");
        }

        [Test]
        [Category("Authenticate With External Provider")]
        public void Authenticate_AccountNotFound_ThrowException()
        {
            AsyncTestDelegate methodInvoke = async () =>
                await service.Authenticate("InvalidEmail", Db.Enums.AuthExternalType.Google);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Account not found"));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With External Provider")]
        public void Authenticate_AccountIsBlocked_ThrowException()
        {
            var existingAccount = new Account
            {
                IsBlocked = true,
                AuthExternal = new[]
                {
                    new AuthExternal
                    {
                        Email = existingEmail,
                        Type = Db.Enums.AuthExternalType.Google,
                    },
                },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            AsyncTestDelegate methodInvoke = async () =>
                await service.Authenticate(existingEmail, Db.Enums.AuthExternalType.Google);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Your account was blocked"));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With External Provider")]
        public async Task Authenticate_AllIsOk_DoesNotThrow()
        {
            var existingAccount = new Account
            {
                AuthExternal = new[]
                {
                    new AuthExternal
                    {
                        Email = existingEmail,
                        Type = Db.Enums.AuthExternalType.Google,
                    },
                },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            mockTokenService.Setup(s => s.GenerateAccessToken(existingAccount)).Returns("AccessToken");
            mockTokenService.Setup(s => s.GenerateRefreshToken()).Returns(new RefreshToken());

            var res = await service.Authenticate(existingEmail, Db.Enums.AuthExternalType.Google);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<AuthenticateResponseModel>(res);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_AccountNotFound_ThrowException()
        {
            AsyncTestDelegate methodInvoke = async () =>
                await service.Authenticate("InvalidEmail", invalidPassword);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Incorrect login or password"));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_AccountIsBloked_ThrowException()
        {
            var existingAccount = new Account
            {
                IsBlocked = true,
                AuthLocal = new AuthLocal { Email = existingEmail },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            AsyncTestDelegate methodInvoke = async () =>
                await service.Authenticate(existingEmail, "anyPassword");

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Your account was blocked."));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_EmailNotConfirmed_ThrowException()
        {
            var existingAccount = new Account
            {
                AuthLocal = new AuthLocal { Email = existingEmail, EmailConfirmed = false },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            AsyncTestDelegate methodInvoke = async () =>
                await service.Authenticate(existingEmail, "anyPassword");

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains($"{existingEmail} is not confirmed, please confirm"));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_InvalidPassword_ThrowException()
        {
            var salt = mockPasswordHasherService.Object.GenerateSalt();
            var existingAccount = new Account
            {
                AuthLocal = new AuthLocal
                {
                    Email = existingEmail,
                    EmailConfirmed = true,
                    Salt = salt,
                    PasswordHash = mockPasswordHasherService.Object.GenerateHash(validPassword, salt),
                },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            AsyncTestDelegate methodInvoke = async () =>
                await service.Authenticate(existingEmail, invalidPassword);

            var ex = Assert.ThrowsAsync<EventsExpressException>(methodInvoke);
            Assert.That(ex.Message.Contains("Incorrect login or password1"));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public async Task AuthenticateLocal_AllIsValid_DoesNotThrow()
        {
            var salt = mockPasswordHasherService.Object.GenerateSalt();
            var existingAccount = new Account
            {
                AuthLocal = new AuthLocal
                {
                    Email = existingEmail,
                    EmailConfirmed = true,
                    Salt = salt,
                    PasswordHash = mockPasswordHasherService.Object.GenerateHash(validPassword, salt),
                },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            mockTokenService.Setup(s => s.GenerateAccessToken(existingAccount)).Returns("AccessToken");
            mockTokenService.Setup(s => s.GenerateRefreshToken()).Returns(new RefreshToken());

            var res = await service.Authenticate(existingEmail, validPassword);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<AuthenticateResponseModel>(res);
        }

        [Test]
        public void ChangePasswordAsync_ValidPassword_DoesNotThrows()
        {
            string salt = mockPasswordHasherService.Object.GenerateSalt();
            User user = new User
            {
                Id = idUser,
                Account = new Account
                {
                    Id = idAccount,
                    AuthLocal = new AuthLocal
                    {
                        Id = AuthLocalId,
                        Salt = salt,
                        PasswordHash = mockPasswordHasherService.Object.GenerateHash(validPassword, salt),
                    },
                },
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            mockSecurityContext.Setup(s => s.GetCurrentAccountId()).Returns(idAccount);

            AsyncTestDelegate methodInvoke = async () =>
                await service.ChangePasswordAsync(validPassword, "newPassword");

            Assert.DoesNotThrowAsync(methodInvoke);
        }

        [Test]
        [Category("CanRegister")]
        public async Task CanRegister_AccountExist_ReturnFalse()
        {
            var existAuthLocal = new AuthLocal { Email = existingEmail };
            Context.AuthLocal.Add(existAuthLocal);
            Context.SaveChanges();

            var res = await service.CanRegister(existingEmail);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.False(res);
        }

        [Test]
        [Category("CanRegister")]
        public async Task CanRegister_AccountNotExist_ReturnTrue()
        {
            var res = await service.CanRegister(existingEmail);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.True(res);
        }

        [Test]
        [Category("Register")]
        public void Register_InvalidModel_ThrowException()
        {
            MockMapper.Setup(s => s.Map<Account>(It.IsAny<RegisterDto>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() =>
                service.Register(new RegisterDto()));
        }

        [Test]
        [Category("Register")]
        public async Task Register_AllIsOk_DoesNotThrow()
        {
            var accountId = Guid.NewGuid();
            var registerDto = new RegisterDto
            {
                Email = "SomeEmail",
                Password = "somePassword",
            };
            var newAccount = new Account { Id = accountId };

            MockMapper.Setup(s => s.Map<Account>(It.IsAny<RegisterDto>())).Returns(newAccount);
            var res = await service.Register(registerDto);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.That(res == accountId);
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
    }
}
