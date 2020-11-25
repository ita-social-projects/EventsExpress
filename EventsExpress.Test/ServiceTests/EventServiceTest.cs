using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using MediatR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventServiceTest : TestInitializer
    {
        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<IEventScheduleService> mockEventScheduleService;
        private static Mock<IMediator> mockMediator;
        private EventService service;
        private List<Event> events;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockEventScheduleService = new Mock<IEventScheduleService>();

            service = new EventService(
                MockUnitOfWork.Object,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object,
                mockEventScheduleService.Object);

            events = new List<Event>
            {
                new Event
                {
                    Id = new Guid("62FA643C-AD14-5BCC-A860-E5A2664B019D"),
                    CityId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl sdmkskdl dsnlndsl",
                    OwnerId = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    PhotoId = new Guid("62FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "SLdndsndj",
                    IsBlocked = false,
                    Categories = null,
                    MaxParticipants = 2147483647,
                },
                new Event
                {
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
                    MaxParticipants = 2147483647,
                    Visitors = new List<UserEvent>()
                    {
                        new UserEvent
                        {
                                    Status = Db.Enums.Status.WillGo,
                                    UserId = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                                    EventId = new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"),
                        },
                    },
                },
            };

            List<User> users = new List<User>()
            {
                new User
                {
                    Id = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    Name = "NameIsExist",
                    Email = "stas@gmail.com",
                },
            };

            MockUnitOfWork.Setup(u => u.EventRepository
                .Delete(It.IsAny<Event>())).Returns((Event i) => events.Where(x => x.Id == i.Id).FirstOrDefault());

            MockUnitOfWork.Setup(u => u.EventRepository
                .Get(It.IsAny<Guid>())).Returns((Guid i) => events.Where(x => x.Id == i).FirstOrDefault());

            MockUnitOfWork.Setup(u => u.UserRepository
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
            MockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.AddUserToEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_UserNotFound_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.AddUserToEvent(new Guid("62FA627C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            StringAssert.Contains("User not found!", result.Result.Message);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_EventNotFound_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.AddUserToEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA644C-AD14-5BCC-A860-E5A2664B019D"));

            StringAssert.Contains("Event not found!", result.Result.Message);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_ReturnTrue()
        {
            MockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.DeleteUserFromEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_UserNotFound_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.DeleteUserFromEvent(new Guid("62FA347C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_EventNotFound_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.EventRepository
                .Get("Visitors")).Returns(events.AsQueryable());

            var result = service.DeleteUserFromEvent(new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"), new Guid("32FA641C-AD14-5BCC-A860-E5A2664B019D"));

            Assert.IsFalse(result.Result.Successed);
        }
    }
}
