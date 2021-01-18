using System;
using System.Collections.Generic;
using System.Security.Claims;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventServiceTest : TestInitializer
    {
        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<ILocationService> mockLocationService;
        private static Mock<IEventScheduleService> mockEventScheduleService;
        private static Mock<IMediator> mockMediator;
        private static Mock<IAuthService> mockAuthService;
        private static Mock<IHttpContextAccessor> httpContextAccessor;

        private EventService service;
        private List<Event> events;
        private EventDTO eventDTO;

        private Guid userId = Guid.NewGuid();
        private Guid firstEventId = Guid.NewGuid();
        private Guid eventId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockLocationService = new Mock<ILocationService>();
            mockEventScheduleService = new Mock<IEventScheduleService>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(new Mock<HttpContext>().Object);
            mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(x => x.GetCurrentUser(It.IsAny<ClaimsPrincipal>()))
                .Returns(new UserDTO { Id = userId });

            service = new EventService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object,
                mockLocationService.Object,
                mockAuthService.Object,
                httpContextAccessor.Object,
                mockEventScheduleService.Object);

            eventDTO = new EventDTO
            {
                Id = firstEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "sjsdnl sdmkskdl dsnlndsl",
                Owners = new List<User>()
                {
                    new User
                    {
                        Id = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    },
                },
                PhotoId = new Guid("62FA647C-AD54-4BCC-A860-E5A2261B019D"),
                Title = "SLdndsndj",
                IsBlocked = false,
                IsPublic = true,
                Categories = null,
                Point = new Point(10.45, 12.34),
                MaxParticipants = 2147483647,
            };

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
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl sdmkskdl dsnlndsl",
                    Owners = new List<EventOwner>()
                    {
                        new EventOwner
                        {
                            UserId = new Guid("62FA647C-AD54-2BCC-A860-E5A2664B013D"),
                        },
                    },
                    PhotoId = new Guid("62FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "SLdndsndj",
                    IsBlocked = false,
                    IsPublic = true,
                    Categories = null,
                    MaxParticipants = 2147483647,
                },
                new Event
                {
                    Id = eventId,
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl fgr sdmkskdl dsnlndsl",
                    Owners = new List<EventOwner>()
                    {
                        new EventOwner
                        {
                            UserId = new Guid("34FA647C-AD54-2BCC-A860-E5A2664B013D"),
                        },
                    },
                    PhotoId = new Guid("11FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "SLdndstrhndj",
                    IsBlocked = false,
                    IsPublic = false,
                    Categories = null,
                    MaxParticipants = 1,
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

            MockMapper.Setup(u => u.Map<EventDTO, LocationDto>(It.IsAny<EventDTO>()))
                .Returns((EventDTO e) => e == null ?
                null :
                new LocationDto
                {
                    Id = Guid.NewGuid(),
                    Point = e.Point,
                });

            MockMapper.Setup(u => u.Map<LocationDto, EventLocation>(It.IsAny<LocationDto>()))
                .Returns((LocationDto e) => e == null ?
                null :
                new EventLocation
                {
                    Point = e.Point,
                    Id = e.Id,
                });

            MockMapper.Setup(u => u.Map<EventDTO, Event>(It.IsAny<EventDTO>()))
                .Returns((EventDTO e) => e == null ?
                null :
                new Event
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    PhotoId = e.PhotoId,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxParticipants = e.MaxParticipants,
                });

            MockMapper.Setup(u => u.Map<EventDTO>(It.IsAny<Event>()))
                .Returns((Event e) => e == null ?
                null :
                new EventDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    PhotoId = (Guid)e.PhotoId,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxParticipants = e.MaxParticipants,
                });
        }

        [Test]
        public void GetEvent_ExistingId_Success()
        {
            Assert.DoesNotThrow(() => service.EventById(firstEventId));
        }

        [Test]
        public void GetEvent_NotExistingId_Failed()
        {
            var result = service.EventById(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void CreateEvent_ValidEvent_Success()
        {
            eventDTO.Id = Guid.Empty;

            Assert.DoesNotThrowAsync(async () => await service.Create(eventDTO));
        }

        [Test]
        public void EditEvent_ValidEvent_Success()
        {
            Assert.DoesNotThrowAsync(async () => await service.Edit(eventDTO));
        }

        [Test]
        public void EditEvent_InvalidEvent_Failed()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.Edit(null));
        }

        [Test]
        public void AddUserToEvent_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.AddUserToEvent(userId, firstEventId));
        }

        [Test]
        public void AddUserToEvent_UserNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(Guid.NewGuid(), eventId));
        }

        [Test]
        public void AddUserToEvent_ToMuchParticipants_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(userId, eventId));
        }

        [Test]
        public void AddUserToEvent_EventNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(userId, Guid.NewGuid()));
        }

        [Test]
        public void DeleteUserFromEvent_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.DeleteUserFromEvent(userId, eventId));
        }

        [Test]
        public void DeleteUserFromEvent_UserNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.DeleteUserFromEvent(Guid.NewGuid(), eventId));
        }

        [Test]
        public void DeleteUserFromEvent_EventNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.DeleteUserFromEvent(userId, Guid.NewGuid()));
        }

        [Test]
        public void ChangeVisitorStatus_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.ChangeVisitorStatus(
                userId,
                eventId,
                UserStatusEvent.Approved));
        }
    }
}
