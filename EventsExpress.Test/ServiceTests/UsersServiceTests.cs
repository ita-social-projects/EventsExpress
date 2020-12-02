using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using MediatR;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class UsersServiceTests : TestInitializer
    {
        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<IMediator> mockMediator;
        private static Mock<IEmailService> mockEmailService;
        private static Mock<ICacheHelper> mockCacheHelper;
        private UserService service;

        private UserDTO existingUserDTO;
        private User existingUser;
        private Role role;

        private Guid roleId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockEmailService = new Mock<IEmailService>();
            mockCacheHelper = new Mock<ICacheHelper>();

            service = new UserService(
                Context,
                MockMapper.Object,
                mockPhotoService.Object,
                mockMediator.Object,
                mockCacheHelper.Object,
                mockEmailService.Object);

            const string existingEmail = "existingEmail@gmail.com";
            var name = "existingName";

            role = new Role
            {
                Id = roleId,
                Name = "Admin",
            };

            existingUser = new User
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
                Role = role,
            };

            existingUserDTO = new UserDTO
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
            };

            Context.Roles.Add(role);
            Context.Users.Add(existingUser);
            Context.SaveChanges();
        }

        [Test]
        public void Create_RepeatEmail_ReturnFalse()
        {
            UserDTO newUser = new UserDTO()
            {
                Id = Guid.NewGuid(),
                Email = existingUserDTO.Email,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(newUser));
        }

        [Test]

        public void Create_ValidDto_ReturnTrue()
        {
            UserDTO newUserDTO = new UserDTO() { Email = "correctemail@example.com" };
            User newUser = new User() { Email = "correctemail@example.com" };

            MockMapper.Setup(m => m
                .Map<User>(newUserDTO))
                    .Returns(newUser);

            Assert.DoesNotThrowAsync(async () => await service.Create(newUserDTO));
        }

        [Test]
        public void Create_InsertFailed_ReturnFalse()
        {
            MockMapper.Setup(m => m
                .Map<User>(existingUserDTO))
                    .Returns(existingUser);

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(existingUserDTO));
        }

        [Test]
        public void ConfirmEmail_NotCorrectUserId_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ConfirmEmail(cache));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConfirmEmail_TokenIsNullOrEmpty_ReturnFalse(string token)
        {
            CacheDTO cache = new CacheDTO()
            {
                UserId = existingUser.Id,
                Token = token,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ConfirmEmail(cache));
        }

        [Test]
        public void ConfirmEmail_ValidCacheDto_ReturnTrue()
        {
            CacheDTO cache = new CacheDTO()
            {
                UserId = existingUser.Id,
                Token = "validToken",
            };

            mockCacheHelper.Setup(u => u.GetValue(cache.UserId))
                .Returns(new CacheDTO { Token = cache.Token });

            Assert.DoesNotThrowAsync(async () => await service.ConfirmEmail(cache));
        }

        [Test]
        public void ConfirmEmail_CachingFailed_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO()
            {
                UserId = existingUser.Id,
                Token = "validToken,",
            };

            mockCacheHelper.Setup(u => u.GetValue(cache.UserId))
                .Returns(new CacheDTO { Token = "invalidToken" });

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ConfirmEmail(cache));
        }

        [Test]
        public void PasswordRecovery_UserNoFoundInDb_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.PasswordRecover(new UserDTO()));
        }

        [Test]
        public void PasswordRecovery_ValidUserDto_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.PasswordRecover(existingUserDTO));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Update_EmailIsNull_ReturnFalse(string email)
        {
            UserDTO newUser = new UserDTO() { Email = email };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Update(newUser));
        }

        [Test]
        public void Update_UserInDbNotFound_Throws()
        {
            existingUserDTO.Id = Guid.NewGuid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Update(existingUserDTO));
        }

        [Test]
        public void Update_UserDtoIsvalid_DoesNotThrow()
        {
            MockMapper.Setup(m => m
                .Map<UserDTO, User>(existingUserDTO))
                    .Returns(existingUser);

            Assert.DoesNotThrowAsync(async () => await service.Update(existingUserDTO));
        }

        [Test]
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
        }

        [Test]
        public void Unblock_InvalidUser_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Unblock(Guid.NewGuid()));
        }

        [Test]
        public void Unblock_AllIsValid_ReturnFalse()
        {
            Assert.DoesNotThrowAsync(async () => await service.Unblock(existingUser.Id));
        }

        [Test]
        public void Block_InvalidUser_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Block(Guid.NewGuid()));
        }

        [Test]
        public void Block_AllIsValid_ReturnFalse()
        {
            Assert.DoesNotThrowAsync(async () => await service.Block(existingUser.Id));
        }
    }
}
