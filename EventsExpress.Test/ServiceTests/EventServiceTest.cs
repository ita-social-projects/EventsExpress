using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Test.ServiceTests.TestClasses.Event;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventServiceTest : TestInitializer
    {
        private static Mock<IEventService> mockEventService;
        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<ILocationService> mockLocationService;
        private static Mock<IEventScheduleService> mockEventScheduleService;
        private static Mock<IMediator> mockMediator;
        private static Mock<IAuthService> mockAuthService;
        private static Mock<IValidator<Event>> mockValidationService;

        private EventService service;
        private List<Event> events;
        private EventLocation eventLocationMap;
        private EventLocation eventLocationMapSecond;
        private EventLocation eventLocationOnline;
        private Guid userId = Guid.NewGuid();
        private Guid eventId = Guid.NewGuid();
        private Guid eventLocationIdMap = Guid.NewGuid();
        private Guid eventLocationIdOnline = Guid.NewGuid();
        private Guid eventLocationIdMapSecond = Guid.NewGuid();
        private double radius = 8;
        private PaginationViewModel model = new PaginationViewModel
        {
            PageSize = 6,
            Page = 1,
        };

        private static LocationDto MapLocationDtoFromEventDto(EventDto eventDto)
        {
            if (eventDto.Type == LocationType.Map)
            {
                return new LocationDto
                {
                    Id = Guid.NewGuid(),
                    Point = eventDto.Point,
                    Type = LocationType.Map,
                };
            }
            else if (eventDto.Type == LocationType.Online)
            {
                return new LocationDto
                {
                    Id = Guid.NewGuid(),
                    OnlineMeeting = eventDto.OnlineMeeting,
                    Type = LocationType.Online,
                };
            }

            return null;
        }

        private static EventLocation MapEventLocationFromLocationDto(LocationDto locationDto)
        {
            if (locationDto.Type == LocationType.Map)
            {
                return new EventLocation
                {
                    Id = Guid.NewGuid(),
                    Point = locationDto.Point,
                    Type = LocationType.Map,
                };
            }
            else if (locationDto.Type == LocationType.Online)
            {
                return new EventLocation
                {
                    Id = Guid.NewGuid(),
                    OnlineMeeting = locationDto.OnlineMeeting,
                    Type = LocationType.Online,
                };
            }

            return null;
        }

        private EventDto DeepCopyDto(EventDto eventDto)
        {
            List<User> users = new List<User>();
            foreach (var owner in eventDto.Owners)
            {
                users.Add(owner);
            }

            return new EventDto
            {
                Id = eventDto.Id,
                DateFrom = eventDto.DateFrom,
                DateTo = eventDto.DateTo,
                Description = eventDto.Description,
                Owners = users,
                Title = eventDto.Title,
                IsPublic = eventDto.IsPublic,
                Categories = eventDto.Categories,
                Point = eventDto.Point,
                MaxParticipants = eventDto.MaxParticipants,
                Type = eventDto.Type,
            };
        }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            mockEventService = new Mock<IEventService>();
            mockPhotoService = new Mock<IPhotoService>();
            mockLocationService = new Mock<ILocationService>();
            mockEventScheduleService = new Mock<IEventScheduleService>();
            mockValidationService = new Mock<IValidator<Event>>();
            mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(x => x.GetCurrentUser())
                .Returns(new UserDto { Id = userId });

            service = new EventService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object,
                mockLocationService.Object,
                mockAuthService.Object,
                mockEventScheduleService.Object,
                mockValidationService.Object);

            eventLocationMap = new EventLocation
            {
                Id = eventLocationIdMap,
                Point = new Point(10.45, 12.34),
                Type = LocationType.Map,
            };

            eventLocationMapSecond = new EventLocation
            {
                Id = eventId,
                Point = new Point(50.45, 30.34),
                Type = LocationType.Map,
            };
            eventLocationOnline = new EventLocation
            {
                Id = eventLocationIdOnline,
                OnlineMeeting = new Uri("http://basin.example.com/#branch"),
                Type = LocationType.Online,
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
                    Id = GetEventExistingId.FirstEventId,
                    EventSchedule = new EventSchedule
                    {
                        IsActive = true,
                        Frequency = 1,
                        Periodicity = Periodicity.Weekly,
                        NextRun = DateTime.Today.AddDays(7),
                    },
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl sdmkskdl dsnlndsl",
                    Owners = new List<EventOwner>()
                    {
                        new EventOwner
                        {
                            UserId = Guid.NewGuid(),
                        },
                    },
                    EventLocationId = eventLocationIdMap,
                    Title = "SLdndsndj",
                    IsPublic = true,
                    Categories = null,
                    MaxParticipants = 2147483647,
                    StatusHistory = new List<EventStatusHistory>()
                    {
                        new EventStatusHistory
                        {
                            EventStatus = EventStatus.Active,
                            CreatedOn = DateTime.Today,
                        },
                    },
                },
                new Event
                {
                    Id = GetEventExistingId.ThirdEventId,

                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "test event",
                    Owners = new List<EventOwner>()
                    {
                        new EventOwner
                        {
                            UserId = Guid.NewGuid(),
                        },
                    },
                    EventLocationId = eventLocationIdMapSecond,
                    Title = "any title",
                    IsPublic = true,
                    Categories = null,
                    MaxParticipants = 8,
                    StatusHistory = new List<EventStatusHistory>()
                    {
                        new EventStatusHistory
                        {
                            EventStatus = EventStatus.Blocked,
                            CreatedOn = DateTime.Today,
                        },
                    },
                },
                new Event
                {
                    Id = GetEventExistingId.SecondEventId,
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl sdmkskdl dsnlndsl",
                    Owners = new List<EventOwner>()
                    {
                        new EventOwner
                        {
                            UserId = userId,
                        },
                    },
                    EventLocationId = eventLocationIdOnline,
                    Title = "SLdndsndj",
                    IsPublic = true,
                    Categories = null,
                    MaxParticipants = 25,
                    StatusHistory = new List<EventStatusHistory>()
                    {
                        new EventStatusHistory
                        {
                            EventStatus = EventStatus.Draft,
                            CreatedOn = DateTime.Today,
                        },
                    },
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
                            UserId = Guid.NewGuid(),
                        },
                    },
                    Title = "SLdndstrhndj",
                    IsPublic = false,
                    Categories = null,
                    MaxParticipants = 1,
                    StatusHistory = new List<EventStatusHistory>()
                    {
                        new EventStatusHistory
                        {
                            EventStatus = EventStatus.Active,
                            CreatedOn = DateTime.Today,
                        },
                    },
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

            Context.EventLocations.Add(eventLocationMap);
            Context.EventLocations.Add(eventLocationMapSecond);
            Context.EventLocations.Add(eventLocationOnline);
            Context.Events.AddRange(events);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<EventDto, LocationDto>(It.IsAny<EventDto>()))
                .Returns((EventDto e) => MapLocationDtoFromEventDto(e));

            MockMapper.Setup(u => u.Map<LocationDto, EventLocation>(It.IsAny<LocationDto>()))
                .Returns((LocationDto e) => MapEventLocationFromLocationDto(e));

            MockMapper.Setup(u => u.Map<EventDto, Event>(It.IsAny<EventDto>()))
                .Returns((EventDto e) => e == null ?
                null :
                new Event
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxParticipants = e.MaxParticipants,
                });

            MockMapper.Setup(u => u.Map<EventDto>(It.IsAny<Event>()))
                .Returns((Event e) => e == null ?
                null :
                new EventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxParticipants = e.MaxParticipants,
                });
        }

        [Test]
        [TestCaseSource(typeof(GetEventExistingId), nameof(GetEventExistingId.TestCasesForGetEvent))]
        [Category("Get Event")]
        public void GetEvent_ExistingId_Success(Guid existingId)
        {
            Assert.DoesNotThrow(() => service.EventById(existingId));
        }

        [Test]
        [Category("Get Event")]
        public void GetEvent_NotExistingId_Failed()
        {
            var result = service.EventById(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        [Category("Get All")]
        public void GetAll_GetEventByLocation_Success()
        {
            EventFilterViewModel eventFilterViewModel = new EventFilterViewModel()
            {
                X = eventLocationMap.Point.X,
                Y = eventLocationMap.Point.Y,
                Radius = radius,
            };
            var count = events.Count;
            service.GetAll(eventFilterViewModel, out count);
            Assert.AreEqual(count, 1);
        }

        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        public void EditNextEvent_Work_Plug(EventDto eventDto)
        {
            var result = service.EditNextEvent(eventDto);

            Assert.IsNotNull(result);
        }

        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        [Category("Create Event")]
        public void CreateEvent_ValidEvent_Success(EventDto eventDto)
        {
            EventDto dto = DeepCopyDto(eventDto);
            dto.Id = Guid.Empty;

            Assert.DoesNotThrowAsync(async () => await service.Create(dto));
            mockPhotoService.Verify(x => x.AddEventPhoto(It.IsAny<IFormFile>(), dto.Id), Times.Once);
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        [Category("Edit Event")]
        public void EditEvent_ValidEvent_Success(EventDto eventDto)
        {
            string testFilePath = @"./Images/valid-image.jpg";
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            eventDto.Photo = file;

            Assert.DoesNotThrowAsync(async () => await service.Edit(eventDto));
            mockPhotoService.Verify(x => x.AddEventPhoto(eventDto.Photo, eventDto.Id));
        }

        [Test]
        [Category("Edit Event")]
        public void EditEvent_InvalidEvent_Failed()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.Edit(null));
        }

        [Test]
        [TestCaseSource(typeof(GetEventExistingId), nameof(GetEventExistingId.TestCasesForAddUserToEvent))]
        [Category("Add user to event")]
        public void AddUserToEvent_ReturnTrue(Guid id)
        {
            Assert.DoesNotThrowAsync(async () => await service.AddUserToEvent(userId, id));
        }

        [Test]
        [Category("Add user to event")]
        public void AddUserToEvent_UserNotFound_Failed()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(eventId, Guid.NewGuid()));
        }

        [Test]
        [Category("Add user to event")]
        public void AddUserToEvent_ToMuchParticipants_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(userId, eventId));
        }

        [Test]
        [Category("Add user to event")]
        public void AddUserToEvent_EventNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(userId, Guid.NewGuid()));
        }

        [Test]
        [Category("Add user to event")]
        public void AddUserToEvent_UserNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(Guid.NewGuid(), userId));
        }

        [Test]
        [Category("Delete user")]
        public void DeleteUserFromEvent_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.DeleteUserFromEvent(userId, eventId));
        }

        [Test]
        [Category("Delete user")]
        public void DeleteUserFromEvent_UserNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.DeleteUserFromEvent(Guid.NewGuid(), eventId));
        }

        [Test]
        [Category("Delete user")]
        public void DeleteUserFromEvent_EventNotFound_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.DeleteUserFromEvent(userId, Guid.NewGuid()));
        }

        [Test]
        [Category("Change visitor status")]
        public void ChangeVisitorStatus_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.ChangeVisitorStatus(
                userId,
                eventId,
                UserStatusEvent.Approved));
        }

        [Test]
        [Category("Future events by user id")]
        public void FutureEventsByUserId_ReturnEvents()
        {
            var events = service.FutureEventsByUserId(Guid.NewGuid(), model);
            Assert.That(events, Is.Not.Null);
        }

        [Test]
        [Category("Past events by user id")]
        public void PastEventsByUserId_ReturnEvents()
        {
            var events = service.PastEventsByUserId(Guid.NewGuid(), model);
            Assert.That(events, Is.Not.Null);
        }

        [Test]
        [Category("Visited events by user id")]
        public void VisitedEventsByUserId_ReturnEvents()
        {
            var events = service.VisitedEventsByUserId(Guid.NewGuid(), model);
            Assert.That(events, Is.Not.Null);
        }

        [Test]
        [Category("Events to go by user id")]
        public void EventsToGoByUserId_ReturnEvents()
        {
            var events = service.EventsToGoByUserId(Guid.NewGuid(), model);
            Assert.That(events, Is.Not.Null);
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        [Test]
        public void CreateDraft_Works()
        {
            service.CreateDraft();
            Assert.AreEqual(5, Context.Events.Count());
        }

        [Test]
        [Category("Publish Event")]
        public void Publish_InvalidId_Throw()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.Publish(Guid.NewGuid()));
            Assert.That(ex.Message, Contains.Substring("Not found"));
        }

        [Test]
        [Category("Publish Event")]
        public void Publish_ValidEvent_Works()
        {
            mockValidationService.Setup(v => v.Validate(It.IsAny<Event>())).Returns(new FluentValidation.Results.ValidationResult());
            Assert.DoesNotThrowAsync(async () => await service.Publish(GetEventExistingId.SecondEventId));
            var statusHistory = Context.Events.Find(GetEventExistingId.SecondEventId).StatusHistory.Last();
            Assert.AreEqual(EventStatus.Active, statusHistory.EventStatus);
            mockMediator.Verify(m => m.Publish(It.IsAny<EventCreatedMessage>(), default), Times.Once());
        }

        [Test]
        [Category("Publish Event")]
        public void Publish_InValidEvent_Throws()
        {
            var validationResultMock = new Mock<FluentValidation.Results.ValidationResult>();
            validationResultMock
                .SetupGet(x => x.IsValid)
                .Returns(() => false);

            mockValidationService.Setup(v => v.Validate(It.IsAny<Event>())).Returns(validationResultMock.Object);
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.Publish(GetEventExistingId.SecondEventId));
            Assert.That(ex.Message, Contains.Substring("validation failed"));
        }

        [Test]
        [Category("Get all drafts")]

        public void GetAllDrafts_Works()
        {
            int x = 1;
            MockMapper.Setup(u => u.Map<IEnumerable<Event>, IEnumerable<EventDto>>(It.IsAny<IEnumerable<Event>>()))
                .Returns((IEnumerable<Event> e) => e?.Select(item => new EventDto { Id = item.Id }));
            mockAuthService.Setup(x => x.GetCurrentUserId())
                .Returns(userId);
            var result = service.GetAllDraftEvents(1, 1, out x);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async System.Threading.Tasks.Task CreateNextEvent_WorksAsync()
        {
            mockEventScheduleService.Setup(e => e.EventScheduleByEventId(GetEventExistingId.FirstEventId)).Returns(new EventScheduleDto()
            {
                IsActive = true,
                Frequency = 1,
                Periodicity = Periodicity.Weekly,
                NextRun = DateTime.Today.AddDays(7),
            });
            var result = await service.CreateNextEvent(GetEventExistingId.FirstEventId);

            Assert.AreNotEqual(Guid.Empty, result);
            var test = Context.Events.Find(result);
            Assert.IsNotNull(test);
            Assert.AreEqual(DateTime.Today.AddDays(7), test.DateFrom);
        }
    }
}
