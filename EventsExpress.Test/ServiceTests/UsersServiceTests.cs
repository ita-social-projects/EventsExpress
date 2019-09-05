using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AutoMapper;
using EventsExpress.Core.IServices;
using MediatR;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using System.Linq;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Test.ServiceTests
{    [TestFixture]
    class UsersServiceTests: TestInitializer
    {
        private UserService service;
        private User user;

        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<IMediator> mockMediator;
        private static Mock<IEmailService> mockEmailService;
        private static Mock<ICacheHelper> mockCacheHelper;
        private static Mock<IEventService> mockEventService;

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
            mockEventService = new Mock<IEventService>();

            service = new UserService(mockUnitOfWork.Object, mockMapper.Object, mockPhotoService.Object, mockMediator.Object, mockCacheHelper.Object, mockEmailService.Object,mockEventService.Object);

            const string existingEmail = "existingEmail@gmail.com";
            var id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var name = "existingName";

            existingUser = new User { Id = id, Name = name, Email = existingEmail };
            existingUserDTO = new UserDTO { Id = id, Name = name, Email = existingEmail };

            

        }

        [Test]
        public void Create_RepeatEmail_ReturnFalse()
        {

            mockUnitOfWork.Setup(u => u.UserRepository
            .Get(""))
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

            mockMapper.Setup(m => m
                .Map<UserDTO, User>(newUserDTO))
                    .Returns(newUser);

            mockUnitOfWork.Setup(u => u.RoleRepository
                .Get(""))
                    .Returns(new List<Role> { new Role { Name = "User" } }.AsQueryable);
            mockUnitOfWork.Setup(u => u.UserRepository
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

            mockMapper.Setup(m => m
                .Map<UserDTO, User>(newUserDTO))
                    .Returns(newUser);

            mockUnitOfWork.Setup(u => u.RoleRepository
                .Get(""))
                    .Returns(new List<Role> { new Role { Name = "User" } }.AsQueryable);
            mockUnitOfWork.Setup(u => u.UserRepository
                .Insert(newUser))
                    .Returns(newUser);


            var result = service.Create(newUserDTO);


            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Verificate_NotCorrectUserId_ReturnFalse()
        {
            
            CacheDTO cache = new CacheDTO() { };
            mockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId));
            var result = service.Verificate(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Verificate_TokenIsNullOrEmpty_ReturnFalse(string token)
        {
            CacheDTO cache = new CacheDTO() { UserId=existingUser.Id, Token = token };


            mockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId))
                .Returns(existingUser);

            
            var result = service.Verificate(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Verificate_ValidCacheDto_ReturnTrue()
        {
            CacheDTO cache = new CacheDTO() { UserId = existingUser.Id, Token = "validToken" };


            mockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId))
                .Returns(existingUser);

            mockCacheHelper.Setup(u => u.GetValue(cache.UserId))
                .Returns(new CacheDTO { Token = cache.Token });

            var result = service.Verificate(cache);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Verificate_CachingFailed_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { UserId = existingUser.Id, Token = "validToken" };


            mockUnitOfWork.Setup(u => u.UserRepository.Get(cache.UserId))
                .Returns(existingUser);

            mockCacheHelper.Setup(u => u.GetValue(cache.UserId))
                .Returns(new CacheDTO { Token = "invalidToken" });

            var result = service.Verificate(cache);

            Assert.IsFalse(result.Result.Successed);
        }

         [Test]
         public void PasswordRecovery_UserdDTONull_ReturnFalse()
         {
             UserDTO newUser = null;
             var result = service.PasswordRecover(newUser);

             Assert.IsFalse(result.Result.Successed);
         }
        [Test]
        public void PasswordRecovery_UserNotFoundInDb_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id));

            var result = service.PasswordRecover(existingUserDTO);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_ValidUserDto_ReturnTrue()
        {
            mockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id))
                .Returns(existingUser);

            var result = service.PasswordRecover(existingUserDTO);

            Assert.IsTrue(result.Result.Successed);
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Update_EmailIsNull_ReturnFalse(string email)
        {
            UserDTO newUser = new UserDTO() { Email=email};
            var result = service.Update(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_UserInDbNotFound_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.UserRepository.Get(existingUserDTO.Id));
                
            
            var result = service.Update(existingUserDTO); 
                
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_UserDtoIsvalid_ReturnTrue()
        {
            mockUnitOfWork.Setup(u => u.UserRepository
           .Get(""))
           .Returns(new List<User> { existingUser }.AsQueryable());

            mockMapper.Setup(m => m
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
            mockUnitOfWork.Setup(u => u.RoleRepository.Get(roleId));

            var result = service.ChangeRole(userId,roleId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_InvalidUser_ReturnFalse()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            mockUnitOfWork.Setup(u => u.RoleRepository.Get(roleId))
            .Returns(new Role() { Name="sameName"});

            mockUnitOfWork.Setup(u => u.UserRepository.Get(userId));

            var result = service.ChangeRole(userId, roleId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeRole_RoleIdAndUserIdIsValid_ReturnTrue()
        {
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            mockUnitOfWork.Setup(u => u.RoleRepository.Get(roleId))
            .Returns(new Role() { Name = "sameName" });

            mockUnitOfWork.Setup(u => u.UserRepository.Get(userId))
                .Returns(new User() {Name="sameName" });

            var result = service.ChangeRole(userId, roleId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Unblock_InvalidUser_ReturnFalse()
        {
            var userId = Guid.NewGuid();

            mockUnitOfWork.Setup(u => u.UserRepository.Get(userId));

            var result = service.Unblock(userId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Unblock_AllIsValid_ReturnFalse()
        {
            var userId = existingUser.Id;

            mockUnitOfWork.Setup(u => u.UserRepository.Get(userId))
                .Returns(existingUser);

            var result = service.Unblock(userId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Block_InvalidUser_ReturnFalse()
        {
            var userId = Guid.NewGuid();

            mockUnitOfWork.Setup(u => u.UserRepository.Get(userId));

            var result = service.Block(userId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Block_AllIsValid_ReturnFalse()
        {
            var userId = existingUser.Id;

            mockUnitOfWork.Setup(u => u.UserRepository.Get(userId))
                .Returns(existingUser);

            var result = service.Block(userId);

            Assert.IsTrue(result.Result.Successed);
        }

    }

}
