using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
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
        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<ILocationService> mockLocationService;
        private static Mock<IEventScheduleService> mockEventScheduleService;
        private static Mock<IMediator> mockMediator;
        private static Mock<IValidator<Event>> mockValidationService;
        private static Mock<ISecurityContext> mockSecurityContextService;

        private EventService service;
        private List<Event> events;
        private List<Rate> rates;
        private EventLocation eventLocationMap;
        private EventLocation eventLocationMapSecond;
        private EventLocation eventLocationOnline;
        private Guid userId = Guid.NewGuid();
        private Guid eventId = Guid.NewGuid();
        private Guid eventLocationIdMap = Guid.NewGuid();
        private Guid eventLocationIdOnline = Guid.NewGuid();
        private Guid eventCategoryId = Guid.NewGuid();
        private Guid eventLocationIdMapSecond = Guid.NewGuid();
        private double radius = 8;
        private byte score = 9;
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

        private FormFile GetPhoto(string filePath)
        {
            string testFilePath = filePath;
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            return file;
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
            mockPhotoService = new Mock<IPhotoService>();
            mockLocationService = new Mock<ILocationService>();
            mockEventScheduleService = new Mock<IEventScheduleService>();
            mockValidationService = new Mock<IValidator<Event>>();
            mockSecurityContextService = new Mock<ISecurityContext>();
            mockSecurityContextService.Setup(x => x.GetCurrentUserId())
                .Returns(userId);

            service = new EventService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object,
                mockLocationService.Object,
                mockEventScheduleService.Object,
                mockValidationService.Object,
                mockSecurityContextService.Object);

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
                        LastRun = DateTime.Today,
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
                    Title = "First event",
                    IsPublic = true,
                    Categories = new List<EventCategory>()
                    {
                        new EventCategory
                        {
                            EventId = GetEventExistingId.FirstEventId,
                            CategoryId = eventCategoryId,
                            Category = new Category() { Id = eventCategoryId, Name = "Meeting" },
                        },
                    },
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
                    Title = "Third event",
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
                    EventSchedule = new EventSchedule
                    {
                        IsActive = true,
                        Frequency = 1,
                        Periodicity = Periodicity.Weekly,
                        LastRun = DateTime.Today,
                        NextRun = DateTime.Today.AddDays(7),
                    },
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
                    Title = "Second event",
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
                    Rates = new List<Rate>()
                    {
                        new Rate
                        {
                            EventId = eventId,
                            UserFromId = userId,
                            Score = 6,
                        },
                    },
                },
            };

            rates = new List<Rate>
            {
                new Rate
                {
                    EventId = eventId,
                    UserFromId = userId,
                    Score = 6,
                },
                new Rate
                {
                    EventId = eventId,
                    UserFromId = Guid.NewGuid(),
                    Score = 9,
                },
                new Rate
                {
                    EventId = GetEventExistingId.FirstEventId,
                    UserFromId = userId,
                    Score = 10,
                },
            };

            Context.EventLocations.Add(eventLocationMap);
            Context.EventLocations.Add(eventLocationMapSecond);
            Context.EventLocations.Add(eventLocationOnline);
            Context.Events.AddRange(events);
            Context.Rates.AddRange(rates);
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

            MockMapper.Setup(u => u.Map<EventScheduleDto>(It.IsAny<EventDto>()))
                .Returns((EventDto e) => e == null ?
                null :
                new EventScheduleDto
                {
                    Id = e.Id,
                    IsActive = true,
                    Frequency = e.Frequency,
                    Periodicity = e.Periodicity,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(7),
                    Event = e,
                    EventId = e.Id,
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
            Assert.AreEqual(1, count);
        }

        [Test]
        [Category("Get All")]
        public void GetAll_GetEventByCategories_Success()
        {
            EventFilterViewModel eventFilterViewModel = new EventFilterViewModel()
            {
                Categories = new List<string>() { eventCategoryId.ToString() },
            };
            var count = events.Count;
            service.GetAll(eventFilterViewModel, out count);
            Assert.AreEqual(1, count);
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        [Category("Edit Next Event")]
        public async System.Threading.Tasks.Task EditNextEvent_Work_PlugAsync(EventDto eventDto)
        {
            mockEventScheduleService.Setup(e => e.EventScheduleByEventId(eventDto.Id)).Returns(new EventScheduleDto()
            {
                IsActive = true,
                Frequency = 1,
                Periodicity = Periodicity.Weekly,
                NextRun = DateTime.Today.AddDays(7),
            });
            eventDto.DateFrom = DateTime.Today.AddDays(7);

            var result = await service.EditNextEvent(eventDto);

            Assert.AreNotEqual(Guid.Empty, result);
            var test = Context.Events.Find(result);
            Assert.IsNotNull(test);
            Assert.AreEqual(result, test.Id);
            Assert.AreEqual(DateTime.Today.AddDays(7), test.DateFrom);
            mockEventScheduleService.Verify(e => e.EventScheduleByEventId(It.IsAny<Guid>()), Times.Once);
            mockEventScheduleService.Verify(e => e.Edit(It.IsAny<EventScheduleDto>()), Times.Once);
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        [Category("Create Next Event")]
        public void CreateEvent_ValidEvent_Success(EventDto eventDto)
        {
            EventDto dto = DeepCopyDto(eventDto);
            dto.Id = Guid.Empty;
            dto.Photo = GetPhoto(@"./Images/valid-image.jpg");

            Assert.DoesNotThrowAsync(async () => await service.Create(dto));
            mockPhotoService.Verify(x => x.ChangeTempToImagePhoto(dto.Id), Times.Once);
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        [Category("Edit Event")]
        public void EditEvent_ValidEvent_Success(EventDto eventDto)
        {
            eventDto.Photo = GetPhoto(@"./Images/valid-image.jpg");

            Assert.DoesNotThrowAsync(async () => await service.Edit(eventDto));
            mockPhotoService.Verify(x => x.ChangeTempToImagePhoto(eventDto.Id), Times.Once);
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
        public void AddUserToEvent_UserNotFound_ThrowsAsync()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(Guid.NewGuid(), GetEventExistingId.SecondEventId));
            Assert.That(ex.Message, Contains.Substring("User not found!"));
        }

        [Test]
        [Category("Add user to event")]
        public void AddUserToEvent_TooMuchParticipants_ThrowsAsync()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(userId, eventId));
            Assert.That(ex.Message, Contains.Substring("Too much participants!"));
        }

        [Test]
        [Category("Add user to event")]
        public void AddUserToEvent_EventNotFound_ThrowsAsync()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.AddUserToEvent(userId, Guid.NewGuid()));
            Assert.That(ex.Message, Contains.Substring("Event not found!"));
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
            Assert.DoesNotThrowAsync(async () => await service.ChangeVisitorStatus(userId, eventId, UserStatusEvent.Approved));
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

            validationResultMock.Object.Errors.Add(new FluentValidation.Results.ValidationFailure("Description", "Field is required!"));

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
            mockSecurityContextService.Setup(x => x.GetCurrentUserId())
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

        [Test]
        [Category("Get Events")]
        public void GetEvents_ExistingIds_ReturnsEvents()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<EventDto>>(It.IsAny<IEnumerable<Event>>()))
                .Returns((IEnumerable<Event> e) => e?.Select(item => new EventDto { Id = item.Id }));
            List<Guid> eventIds = new List<Guid> { GetEventExistingId.FirstEventId, GetEventExistingId.SecondEventId };

            var events = service.GetEvents(eventIds, model);

            Assert.That(events, Is.Not.Null);
            Assert.AreEqual(2, events.Count());
        }

        [Test]
        [Category("Get Events")]
        public void GetEvents_NotExistingIds_ReturnsEmpty()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<EventDto>>(It.IsAny<IEnumerable<Event>>()))
                .Returns((IEnumerable<Event> e) => e?.Select(item => new EventDto { Id = item.Id }));
            List<Guid> eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            var events = service.GetEvents(eventIds, model);

            Assert.That(events, Is.Not.Null);
            Assert.AreEqual(0, events.Count());
        }

        [Test]
        [Category("Set rate")]
        public void SetRate_ValidIds_UpdatesExistingRate()
        {
            Assert.DoesNotThrowAsync(async () => await service.SetRate(userId, eventId, score));
            var rate = Context.Events.Find(eventId).Rates.Last();
            Assert.AreEqual(score, rate.Score);
        }

        [Test]
        [Category("Set rate")]
        public void SetRate_ValidIds_SetsNewRate()
        {
            Assert.DoesNotThrowAsync(async () => await service.SetRate(userId, GetEventExistingId.ThirdEventId, score));
            var rate = Context.Events.Find(GetEventExistingId.ThirdEventId).Rates.Last();
            Assert.AreEqual(score, rate.Score);
        }

        [Test]
        [Category("Set rate")]
        public void SetRate_InvalidEventId_ThrowsAsync()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.SetRate(userId, Guid.NewGuid(), score));
        }

        [Test]
        [Category("Get rate from user")]
        public void GetRateFromUser_ValidIds_ReturnsTrue()
        {
            var result = service.GetRateFromUser(userId, eventId);

            Assert.AreEqual(rates[0].Score, result);
        }

        [Test]
        [Category("Get rate from user")]
        public void GetRateFromUser_InvalidIds_ReturnsZero()
        {
            var result = service.GetRateFromUser(Guid.NewGuid(), Guid.NewGuid());

            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("Get rate")]
        public void GetRate_ValidEventId_ReturnsTrue()
        {
            var expectedAverageValue = 7d;

            var result = service.GetRate(eventId);

            Assert.AreEqual(expectedAverageValue, result);
        }

        [Test]
        [Category("Get rate")]
        public void GetRate_InvalidEventId_ReturnsZero()
        {
            var result = service.GetRate(Guid.NewGuid());

            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("User is visitor")]
        public void UserIsVisitor_ValidUserId_ReturnsTrue()
        {
            var result = service.UserIsVisitor(userId, eventId);

            Assert.AreEqual(true, result);
        }

        [Test]
        [Category("User is visitor")]
        public void UserIsVisitor_InvalidUserId_ReturnsFalse()
        {
            var result = service.UserIsVisitor(Guid.NewGuid(), eventId);

            Assert.AreEqual(false, result);
        }

        [Test]
        [Category("User is visitor")]
        public void UserIsVisitor_InvalidEventId_ReturnsFalse()
        {
            var result = service.UserIsVisitor(userId, Guid.NewGuid());

            Assert.AreEqual(false, result);
        }

        [Test]
        [Category("Event exists")]
        public void EventExists_ExistingId_ReturnsTrue()
        {
            var result = service.Exists(GetEventExistingId.FirstEventId);
            Assert.AreEqual(true, result);
        }

        [Test]
        [Category("Event exists")]
        public void EventExists_NotExistingEvent_ReturnsFalse()
        {
            var result = service.Exists(Guid.NewGuid());

            Assert.AreEqual(false, result);
        }
    }
}
