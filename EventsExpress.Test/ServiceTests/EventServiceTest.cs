using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    class EventServiceTest : TestInitializer
    {
        private EventService service;
        private User user;
        private List<Event> events;
        private static Mock<IHostingEnvironment> mockAppEnvironment;
        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<IMediator> mockMediator;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockAppEnvironment = new Mock<IHostingEnvironment>();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();

            service = new EventService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockAppEnvironment.Object,
                mockMediator.Object,
                mockPhotoService.Object);

            events = new List<Event>
            {
                new Event{
                    Id = new Guid("62FA643C-AD14-5BCC-A860-E5A2664B019D"),
                    CityId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl sdmkskdl dsnlndsl",
                    OwnerId = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    PhotoId = new Guid("62FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "SLdndsndj",
                    IsBlocked = false,
                    Categories = null
                    },
                new Event{
                    Id = new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"),
                    CityId = new Guid("31FA647C-AD54-4BCC-A860-E5A2664B019D"),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl fgr sdmkskdl dsnlndsl",
                    OwnerId = new Guid("34FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    PhotoId = new Guid("11FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "SLdndstrhndj",
                    IsBlocked = false,
                    Categories = null,
                    Visitors = new List<UserEvent>(){
                        new UserEvent {
                                    Status = Db.Enums.Status.WillGo,
                                    UserId = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                                    EventId = new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D")
                                    }
                        }
                    }
            };

            List<User> users = new List<User>()
            {
                new User
                {
                    Id = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    Name = "NameIsExist",
                    Email = "stas@gmail.com"
                }
            };

            //mockPhotoService.Setup(p => p.AddPhoto(It.IsAny<IFormFile>())).Returns(new Photo { Id = new Guid("11FA647C-AD54-4BCC-A860-E5A2261B019D") });


            mockUnitOfWork.Setup(u => u.EventRepository
                .Delete(It.IsAny<Event>())).Returns((Event i) => events.Where(x => x.Id == i.Id).FirstOrDefault());

            mockUnitOfWork.Setup(u => u.EventRepository
                .Get(It.IsAny<Guid>())).Returns((Guid i) => events.Where(x => x.Id == i).FirstOrDefault());
                                                                         
            mockUnitOfWork.Setup(u => u.UserRepository
                .Get(It.IsAny<Guid>())).Returns((Guid i) => users.Where(x => x.Id == i).FirstOrDefault());

        }


        [Test]
        public void DeleteEvent_ReturnTrue()
        {

            var result = service.Delete(new Guid("62FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void DeleteEvent_ReturnFalse()
        {

            var result = service.Delete(new Guid("12FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_ReturnTrue()
        {

            mockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.AddUserToEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_UserNotFound_ReturnFalse()
        {

            mockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.AddUserToEvent(new Guid("62FA627C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            StringAssert.Contains("User not found!", result.Result.Message);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_EventNotFound_ReturnFalse()
        {

            mockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.AddUserToEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA644C-AD14-5BCC-A860-E5A2664B019D"));

            StringAssert.Contains("Event not found!", result.Result.Message);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_ReturnTrue()
        {
            mockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.DeleteUserFromEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_UserNotFound_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.DeleteUserFromEvent(new Guid("62FA347C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_EventNotFound_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.DeleteUserFromEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA641C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsFalse(result.Result.Successed);
        }
                        
    }
}
