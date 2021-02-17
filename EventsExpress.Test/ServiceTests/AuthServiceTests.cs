using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Helpers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class AuthServiceTests : TestInitializer
    {
        private Mock<IUserService> mockUserService;
        private Mock<ITokenService> mockTokenService;
        private AuthService service;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockUserService = new Mock<IUserService>();
            mockTokenService = new Mock<ITokenService>();
            service = new AuthService(mockUserService.Object, mockTokenService.Object);
        }

        [Test]
        public void ChangePasswordAsync_InvalidPassword_Throws()
        {
            string salt = PasswordHasher.GenerateSalt();
            string validPassword = "validPassword";
            UserDto userDto = new UserDto
            {
                Salt = salt,
                PasswordHash = PasswordHasher.GenerateHash(validPassword, salt),
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangePasswordAsync(userDto, "invalidOldPassword", "newPassword"));
        }

        [Test]
        public void ChangePasswordAsync_ValidPassword_DoesNotThrows()
        {
            string salt = PasswordHasher.GenerateSalt();
            string validPassword = "validPassword";
            UserDto userDto = new UserDto
            {
                Salt = salt,
                PasswordHash = PasswordHasher.GenerateHash(validPassword, salt),
            };

            Assert.DoesNotThrowAsync(async () => await service.ChangePasswordAsync(userDto, validPassword, "newPassword"));
        }

        [Test]
        public async Task ChangePasswordAsync_ValidPassword_CallsUpdateUser()
        {
            string salt = PasswordHasher.GenerateSalt();
            string validPassword = "validPassword";
            UserDto userDto = new UserDto
            {
                Salt = salt,
                PasswordHash = PasswordHasher.GenerateHash(validPassword, salt),
            };

            await service.ChangePasswordAsync(userDto, validPassword, "newPassword");
            mockUserService.Verify(us => us.Update(It.IsAny<UserDto>()), Times.Once);
        }
    }
}
