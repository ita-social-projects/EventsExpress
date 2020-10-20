using System;
using System.Collections.Generic;
using System.Linq;
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

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockEmailService = new Mock<IEmailService>();
            mockCacheHelper = new Mock<ICacheHelper>();

            service = new UserService(MockUnitOfWork.Object, MockMapper.Object, mockPhotoService.Object, mockMediator.Object, mockCacheHelper.Object, mockEmailService.Object);

            const string existingEmail = "existingEmail@gmail.com";
            var id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var name = "existingName";

            existingUser = new User { Id = id, Name = name, Email = existingEmail };
            existingUserDTO = new UserDTO { Id = id, Name = name, Email = existingEmail };
        }

        [Test]
        public void Create_RepeatEmail_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.UserRepository
            .Get(string.Empty))
            .Returns(new List<User> { existingUser }.AsQueryable());

            UserDTO newUser = new UserDTO() { Id = Guid.NewGuid(), Email = existingUserDTO.Email };

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

            MockUnitOfWork.Setup(u => u.RoleRepository
            .Get(string.Empty))
                .Returns(new List<Role> { new Role() { Name = "SameRole" } }.AsQueryable);

            MockUnitOfWork.Setup(u => u.UserRepository
                .Insert(newUser))
                    .Returns(new User { Id = Guid.NewGuid(), Email = newUser.Email });

            var result = service.Create(newUserDTO);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]

        public void Create_InsertFailed_ReturnFalse()
        {
            UserDTO newUserDTO = new UserDTO() { Email = "correctemail@example.com" };
            User newUser = new User() { Email = "correctemail@example.com" };

            MockMapper.Setup(m => m
                .Map<User>(newUserDTO))
                    .Returns(newUser);

            MockUnitOfWork.Setup(u => u.RoleRepository
                .Get(string.Empty))
                    .Returns(new List<Role> { new Role { Name = "User" } }.AsQueryable);
            MockUnitOfWork.Setup(u => u.UserRepository
                .Insert(newUser))
                    .Returns(newUser);

            var result = service.Create(newUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ConfirmEmail_NotCorrectUserId_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { };
            MockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId));
            var result = service.ConfirmEmail(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConfirmEmail_TokenIsNullOrEmpty_ReturnFalse(string token)
        {
            CacheDTO cache = new CacheDTO() { UserId = existingUser.Id, Token = token };

            MockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId))
                .Returns(existingUser);

            var result = service.ConfirmEmail(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ConfirmEmail_ValidCacheDto_ReturnTrue()
        {
            CacheDTO cache = new CacheDTO() { UserId = existingUser.Id, Token = "validToken" };

            MockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId))
                .Returns(existingUser);

            mockCacheHelper.Setup(u => u.GetValue(cache.UserId))
                .Returns(new CacheDTO { Token = cache.Token });

            var result = service.ConfirmEmail(cache);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void ConfirmEmail_CachingFailed_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { UserId = existingUser.Id, Token = "validToken" };

            MockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId))
                .Returns(existingUser);

            mockCacheHelper.Setup(u => u.GetValue(cache.UserId))
                .Returns(new CacheDTO { Token = "invalidToken" });

            var result = service.ConfirmEmail(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_UserNoFoundInDb_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id));

            var result = service.PasswordRecover(existingUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_UserNotFoundInDb_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id));

            var result = service.PasswordRecover(existingUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_ValidUserDto_ReturnTrue()
        {
            MockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id))
                .Returns(existingUser);

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
            MockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id));

            var result = service.Update(existingUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_UserDtoIsvalid_ReturnTrue()
        {
            MockUnitOfWork.Setup(u => u.UserRepository
           .Get(string.Empty))
           .Returns(new List<User> { existingUser }.AsQueryable());

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
            MockUnitOfWork.Setup(u => u.RoleRepository.Get(roleId));

            var result = service.ChangeRole(userId, roleId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_InvalidUser_ReturnFalse()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            MockUnitOfWork.Setup(u => u.RoleRepository.Get(roleId))
            .Returns(new Role() { Name = "sameName" });

            MockUnitOfWork.Setup(u => u.UserRepository.Get(userId));

            var result = service.ChangeRole(userId, roleId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_RoleIdAndUserIdIsValid_ReturnTrue()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            MockUnitOfWork.Setup(u => u.RoleRepository.Get(roleId))
            .Returns(new Role() { Name = "sameName" });

            MockUnitOfWork.Setup(u => u.UserRepository.Get(userId))
                .Returns(new User() { Name = "sameName" });

            var result = service.ChangeRole(userId, roleId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Unblock_InvalidUser_ReturnFalse()
        {
            var userId = Guid.NewGuid();

            MockUnitOfWork.Setup(u => u.UserRepository.Get(userId));

            var result = service.Unblock(userId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Unblock_AllIsValid_ReturnFalse()
        {
            var userId = existingUser.Id;

            MockUnitOfWork.Setup(u => u.UserRepository.Get(userId))
                .Returns(existingUser);

            var result = service.Unblock(userId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Block_InvalidUser_ReturnFalse()
        {
            var userId = Guid.NewGuid();

            MockUnitOfWork.Setup(u => u.UserRepository.Get(userId));

            var result = service.Block(userId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Block_AllIsValid_ReturnFalse()
        {
            var userId = existingUser.Id;

            MockUnitOfWork.Setup(u => u.UserRepository.Get(userId))
                .Returns(existingUser);

            var result = service.Block(userId);

            Assert.IsTrue(result.Result.Successed);
        }
    }
}
