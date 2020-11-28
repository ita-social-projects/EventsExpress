using System;
using System.Collections.Generic;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
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

        private Guid userId = Guid.NewGuid();
        private Guid firstEventId = Guid.NewGuid();
        private Guid eventId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockEventScheduleService = new Mock<IEventScheduleService>();

            service = new EventService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object,
                mockEventScheduleService.Object);

            List<User> users = new List<User>()
            {
                new User
                {
                    Id = userId,
                    Name = "NameIsExist",
                    Email = "stas@gmail.com",
                },
            };

            events = new List<Event>
            {
                new Event
                {
                    Id = firstEventId,
                    CityId = Guid.NewGuid(),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "...",
                    OwnerId = Guid.NewGuid(),
                    PhotoId = Guid.NewGuid(),
                    Title = "Title",
                    IsBlocked = false,
                    IsPublic = true,
                    Categories = null,
                    MaxParticipants = 2147483647,
                },
                new Event
                {
                    Id = eventId,
                    CityId = Guid.NewGuid(),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl fgr sdmkskdl dsnlndsl",
                    OwnerId = Guid.NewGuid(),
                    PhotoId = Guid.NewGuid(),
                    Title = "Title",
                    IsBlocked = false,
                    IsPublic = false,
                    Categories = null,
                    MaxParticipants = 2147483647,
                    Visitors = new List<UserEvent>()
                    {
                        new UserEvent
                        {
                                    UserStatusEvent = UserStatusEvent.Pending,
                                    Status = Status.WillGo,
                                    UserId = userId,
                                    User = users[0],
                                    EventId = eventId,
                        },
                    },
                },
            };

            Context.Events.AddRange(events);
            Context.SaveChanges();
        }

        [Test]
        public void AddUserToEvent_ReturnTrue()
        {
            var result = service.AddUserToEvent(userId, firstEventId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_UserNotFound_ReturnFalse()
        {
            var result = service.AddUserToEvent(Guid.NewGuid(), eventId);

            StringAssert.Contains("User not found!", result.Result.Message);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void AddUserToEvent_EventNotFound_ReturnFalse()
        {
            var result = service.AddUserToEvent(userId, Guid.NewGuid());

            StringAssert.Contains("Event not found!", result.Result.Message);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_ReturnTrue()
        {
            var result = service.DeleteUserFromEvent(userId, eventId);

            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_UserNotFound_ReturnFalse()
        {
            var result = service.DeleteUserFromEvent(Guid.NewGuid(), eventId);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void DeleteUserFromEvent_EventNotFound_ReturnFalse()
        {
            var result = service.DeleteUserFromEvent(userId, Guid.NewGuid());

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void ChangeVisitorStatus_ReturnTrue()
        {
            var test = service.ChangeVisitorStatus(
                userId,
                eventId,
                UserStatusEvent.Approved);

            Assert.IsTrue(test.Result.Successed);
        }
    }
}
