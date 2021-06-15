using System;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
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
        private const string Name = "existingName";
        private const string ExistingEmail = "existingEmail@gmail.com";
        private const string ValidPassword = "validPassword";
        private const string InvalidPassword = "invalidPassword";
        private static readonly Guid AuthLocalId = ConfirmEmail.AuthLocalId;
        private readonly Guid idUser = Guid.NewGuid();
        private readonly Guid idAccount = Guid.NewGuid();

        private Mock<IUserService> mockUserService;
        private Mock<ITokenService> mockTokenService;
        private Mock<ICacheHelper> mockCacheHelper;
        private Mock<IEmailService> mockEmailService;
        private Mock<IPasswordHasher> mockPasswordHasherService;
        private Mock<IMediator> mockMediator;
        private Mock<ISecurityContext> mockSecurityContext;
        private AuthService service;

        private UserDto existingUserDto;
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
                Name = Name,
                Email = ExistingEmail,
            };

            existingUserDto = new UserDto
            {
                Id = AuthLocalId,
                Name = Name,
                Email = ExistingEmail,
            };

            Context.Users.Add(existingUser);
            Context.SaveChanges();

            mockPasswordHasherService.Setup(s => s.GenerateSalt()).Returns("salt");
            mockPasswordHasherService.Setup(s => s.GenerateHash(ValidPassword, "salt")).Returns("hash");
        }

        [Test]
        [Category("Authenticate With External Provider")]
        public void Authenticate_AccountNotFound_ThrowException()
        {
            async Task MethodInvoke() => await service.Authenticate("InvalidEmail", Db.Enums.AuthExternalType.Google);

            var ex = Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
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
                        Email = ExistingEmail,
                        Type = Db.Enums.AuthExternalType.Google,
                    },
                },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            async Task MethodInvoke() => await service.Authenticate(ExistingEmail, Db.Enums.AuthExternalType.Google);

            var ex = Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
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
                        Email = ExistingEmail,
                        Type = Db.Enums.AuthExternalType.Google,
                    },
                },
            };
            Context.Accounts.Add(existingAccount);
            await Context.SaveChangesAsync();

            mockTokenService.Setup(s => s.GenerateAccessToken(existingAccount)).Returns("AccessToken");
            mockTokenService.Setup(s => s.GenerateRefreshToken()).Returns(new RefreshToken());

            var res = await service.Authenticate(ExistingEmail, Db.Enums.AuthExternalType.Google);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<AuthenticateResponseModel>(res);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_AccountNotFound_ThrowException()
        {
            async Task MethodInvoke() => await service.Authenticate("InvalidEmail", InvalidPassword);

            var ex = Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
            Assert.That(ex.Message.Contains("Incorrect login or password"));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_AccountIsBlocked_ThrowException()
        {
            var existingAccount = new Account
            {
                IsBlocked = true,
                AuthLocal = new AuthLocal { Email = ExistingEmail },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            async Task MethodInvoke() => await service.Authenticate(ExistingEmail, "anyPassword");

            var ex = Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
            Assert.That(ex.Message.Contains("Your account was blocked."));
            mockTokenService.Verify(s => s.GenerateRefreshToken(), Times.Never);
        }

        [Test]
        [Category("Authenticate With Local Provider")]
        public void AuthenticateLocal_EmailNotConfirmed_ThrowException()
        {
            var existingAccount = new Account
            {
                AuthLocal = new AuthLocal { Email = ExistingEmail, EmailConfirmed = false },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            async Task MethodInvoke() => await service.Authenticate(ExistingEmail, "anyPassword");

            var ex = Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
            Assert.That(ex.Message.Contains($"{ExistingEmail} is not confirmed, please confirm"));
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
                    Email = ExistingEmail,
                    EmailConfirmed = true,
                    Salt = salt,
                    PasswordHash = mockPasswordHasherService.Object.GenerateHash(ValidPassword, salt),
                },
            };
            Context.Accounts.Add(existingAccount);
            Context.SaveChanges();

            async Task MethodInvoke() => await service.Authenticate(ExistingEmail, InvalidPassword);

            var ex = Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
            Assert.That(ex.Message.Contains("Incorrect login or password"));
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
                    Email = ExistingEmail,
                    EmailConfirmed = true,
                    Salt = salt,
                    PasswordHash = mockPasswordHasherService.Object.GenerateHash(ValidPassword, salt),
                },
            };
            Context.Accounts.Add(existingAccount);
            await Context.SaveChangesAsync();

            mockTokenService.Setup(s => s.GenerateAccessToken(existingAccount)).Returns("AccessToken");
            mockTokenService.Setup(s => s.GenerateRefreshToken()).Returns(new RefreshToken());

            var res = await service.Authenticate(ExistingEmail, ValidPassword);

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
                        PasswordHash = mockPasswordHasherService.Object.GenerateHash(ValidPassword, salt),
                    },
                },
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            mockSecurityContext.Setup(s => s.GetCurrentAccountId()).Returns(idAccount);

            async Task MethodInvoke() => await service.ChangePasswordAsync(ValidPassword, "newPassword");

            Assert.DoesNotThrowAsync(MethodInvoke);
        }

        [Test]
        [Category("CanRegister")]
        public async Task CanRegister_AccountExist_ReturnFalse()
        {
            var existAuthLocal = new AuthLocal { Email = ExistingEmail };
            Context.AuthLocal.Add(existAuthLocal);
            await Context.SaveChangesAsync();

            var res = await service.CanRegister(ExistingEmail);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.False(res);
        }

        [Test]
        [Category("CanRegister")]
        public async Task CanRegister_AccountNotExist_ReturnTrue()
        {
            var res = await service.CanRegister(ExistingEmail);

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
                Key = existingUser.Id.ToString(),
                Value = token,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.EmailConfirmAndAuthenticate(Guid.Parse(cache.Key), token));
        }

        [Test]
        public void ConfirmEmail_ValidCacheDto_ReturnTrue()
        {
            // Arrange
            CacheDto cache = new CacheDto()
            {
                Key = existingUser.Id.ToString(),
                Value = "validToken",
            };

            var authLocal = new AuthLocal
            {
                Id = AuthLocalId,
                Account = new Account(),
            };

            Context.AuthLocal.Add(authLocal);
            Context.SaveChanges();

            mockCacheHelper.Setup(ch => ch.GetValue(cache.Key))
                .Returns(new CacheDto { Value = cache.Value });
            mockTokenService.Setup(ts => ts.GenerateAccessToken(It.IsAny<Account>()))
                .Returns("AccessToken");
            mockTokenService.Setup(ts => ts.GenerateRefreshToken())
                .Returns(new RefreshToken());

            // Act
            async Task MethodInvoke() => await service.EmailConfirmAndAuthenticate(Guid.Parse(cache.Key), cache.Value);

            // Assert
            Assert.DoesNotThrowAsync(MethodInvoke);
        }

        [Test]
        [TestCaseSource(typeof(ConfirmEmail), nameof(ConfirmEmail.TestCases))]
        public void ConfirmEmail_CachingFailed_Throws(Guid id, string token)
        {
            // Arrange
            mockCacheHelper.Setup(u => u.GetValue(It.IsAny<string>()));

            // Act
            async Task MethodInvoke() => await service.EmailConfirmAndAuthenticate(id, token);

            // Assert
            Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
        }

        [Test]
        public void PasswordRecovery_UserNoFoundInDb_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.PasswordRecover(new UserDto().Email));
        }

        [Test]
        public void PasswordRecovery_ValidUserDto_ReturnTrue()
        {
            // Arrange
            var authLocal = new AuthLocal
            {
                Id = AuthLocalId,
                Account = new Account(),
                Email = existingUserDto.Email,
            };

            Context.AuthLocal.Add(authLocal);
            Context.SaveChanges();

            // Act
            async Task MethodInvoke() => await service.PasswordRecover(existingUserDto.Email);

            // Assert
            Assert.DoesNotThrowAsync(MethodInvoke);
        }
    }
}
