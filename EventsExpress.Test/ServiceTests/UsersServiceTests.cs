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
        private static Mock<CacheHelper> mockCacheHelper;
        private static Mock<IEventService> mockEventService;

        UserDTO userDTO;
        

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockEmailService = new Mock<IEmailService>();
            mockCacheHelper = new Mock<CacheHelper>();
            mockEventService = new Mock<IEventService>();

            service = new UserService(mockUnitOfWork.Object, mockMapper.Object, mockPhotoService.Object, mockMediator.Object, mockCacheHelper.Object, mockEmailService.Object,mockEventService.Object);

            userDTO = new UserDTO() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "NameIsExist" };
            user = new User() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "NameIsExist" };

            mockUnitOfWork.Setup(u => u.UserRepository
            .Get("")).Returns(new List<User>()
                {
                    new User { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),Email="aaa@gmail.com" ,Name = "NameIsExist" }
                }
                .AsQueryable());

        }

        [Test]
        public void Create_RepeatEmail_ReturnFalse()
        {
            UserDTO newUser = new UserDTO() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019A"), Email = "aaa@gmail.com" };

            var result = service.Create(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Verificate_CacheDtoNull_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { };
            var result = service.Verificate(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Verificate_CacheDtoEmpty_ReturnFalse()
        {
            CacheDTO cache = new CacheDTO() { Token=""};
            var result = service.Verificate(cache);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecovery_UserdDTONull_ReturnFalse()
        {
            UserDTO newUser = new UserDTO() { };
            var result = service.PasswordRecover(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_EmailIsNull_ReturnFalse()
        {
            UserDTO newUser = new UserDTO() { };
            var result = service.Update(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_EmailIsEmpty_ReturnFalse()
        {
            UserDTO newUser = new UserDTO() { Email=""};
            var result = service.Update(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Update_Success_ReturnAnyException()
        {
            mockMapper.Setup(m => m.Map<UserDTO, User>(userDTO))
                .Returns(user);

            mockUnitOfWork.Setup(u => u.UserRepository.Insert(user));
            mockUnitOfWork.Setup(u => u.SaveAsync());

            Assert.DoesNotThrowAsync(async()=>await service.Update(userDTO));
        }

         [Test]
         public void Update_InvalidId_ReturnFalse()
        {
            UserDTO newUser = new UserDTO() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };

            var result = service.Update(newUser);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void PasswordRecocovery_UserNull_ReturnFalse()
        {

        }

    }

}
