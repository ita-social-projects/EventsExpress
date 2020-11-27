using System;
using EventsExpress.Core.DTOs;
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

            var result = service.Create(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]

        public void Create_ValidDto_ReturnTrue()
        {
            UserDTO newUserDTO = new UserDTO() { Email = "correctemail@example.com" };
            User newUser = new User() { Email = "correctemail@example.com" };

            MockMapper.Setup(m => m
                .Map<User>(newUserDTO))
                    .Returns(newUser);

            var result = service.Create(newUserDTO);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Create_InsertFailed_ReturnFalse()
        {
            MockMapper.Setup(m => m
                .Map<User>(existingUserDTO))
                    .Returns(existingUser);

            var result = service.Create(existingUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ConfirmEmail_NotCorrectUserId_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { };

            var result = service.ConfirmEmail(cache);

            Assert.IsFalse(result.Result.Successed);
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

            var result = service.ConfirmEmail(cache);

            Assert.IsFalse(result.Result.Successed);
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

            var result = service.ConfirmEmail(cache);

            Assert.IsTrue(result.Result.Successed);
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

            var result = service.ConfirmEmail(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_UserNoFoundInDb_ReturnFalse()
        {
            var result = service.PasswordRecover(new UserDTO());

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_ValidUserDto_ReturnTrue()
        {
            var result = service.PasswordRecover(existingUserDTO);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Update_EmailIsNull_ReturnFalse(string email)
        {
            UserDTO newUser = new UserDTO() { Email = email };
            var result = service.Update(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_UserInDbNotFound_ReturnFalse()
        {
            var result = service.Update(existingUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_UserDtoIsvalid_ReturnTrue()
        {
            MockMapper.Setup(m => m
                .Map<UserDTO, User>(existingUserDTO))
                    .Returns(existingUser);

            var result = service.Update(existingUserDTO);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_InvalidRole_ReturnFalse()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var result = service.ChangeRole(userId, roleId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_InvalidUser_ReturnFalse()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var result = service.ChangeRole(userId, roleId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_RoleIdAndUserIdIsValid_ReturnTrue()
        {
            var result = service.ChangeRole(userId, roleId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Unblock_InvalidUser_ReturnFalse()
        {
            var userId = Guid.NewGuid();

            var result = service.Unblock(userId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Unblock_AllIsValid_ReturnFalse()
        {
            var userId = existingUser.Id;

            var result = service.Unblock(userId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Block_InvalidUser_ReturnFalse()
        {
            var userId = Guid.NewGuid();

            var result = service.Block(userId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Block_AllIsValid_ReturnFalse()
        {
            var userId = existingUser.Id;

            var result = service.Block(userId);

            Assert.IsTrue(result.Result.Successed);
        }
    }
}
