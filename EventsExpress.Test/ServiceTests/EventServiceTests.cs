using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Enums;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Test.ServiceTests.TestClasses.Event;
using MediatR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventServiceTests : TestInitializer
    {
        private Mock<IPhotoService> mockPhotoService;
        private Mock<ILocationService> mockLocationService;
        private Mock<IEventScheduleService> mockEventScheduleService;
        private Mock<IMediator> mockMediator;
        private Mock<ISecurityContext> mockSecurityContext;

        private IEventService service;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockLocationService = new Mock<ILocationService>();
            mockEventScheduleService = new Mock<IEventScheduleService>();
            mockSecurityContext = new Mock<ISecurityContext>();

            service = new EventService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object,
                mockLocationService.Object,
                mockEventScheduleService.Object,
                mockSecurityContext.Object);

            Context.Locations.AddRange(EventTestData.Locations);
            Context.Events.AddRange(EventTestData.Events);
            Context.Rates.AddRange(EventTestData.Rates);
            Context.Users.AddRange(EventTestData.Users);
            Context.SaveChanges();

            MockMapper.Setup(m => m.Map<EventDto, Event>(It.IsAny<EventDto>()))
                .Returns<EventDto>(EventTestHelpers.MapEventFromEventDto);

            mockSecurityContext.Setup(c => c.GetCurrentUserId())
                .Returns(EventTestData.FirstUserId);
        }

        [Test]
        [TestCaseSource(typeof(GetEventExistingId))]
        public void EventById_ExistingId_ExecutesSuccessfully(Guid existingId)
        {
            Assert.DoesNotThrow(() => service.EventById(existingId));
        }

        [Test]
        public void EventById_NotExistingId_ReturnsNull()
        {
            var result = service.EventById(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetAll_WithEmptyFilter_ReturnsAllEventsExceptDrafts()
        {
            const int expectedCount = 3;
            var filterModel = new EventFilterViewModel();

            service.GetAll(filterModel, out int actualCount);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCaseSource(typeof(TestCasesForGetAll))]
        public void GetAll_WithAppliedFilters_ReturnsSingleElement(EventFilterViewModel filterModel)
        {
            const int expectedCount = 1;

            service.GetAll(filterModel, out int actualCount);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public void GetAll_DifferentOrderApplied_ReturnsElementsInCorrectOrder()
        {
            MockMapper.Setup(m => m.Map<IEnumerable<EventDto>>(It.IsAny<IEnumerable<Event>>()))
                .Returns<IEnumerable<Event>>(ev => ev.Select(EventTestHelpers.MapEventDtoFromEvent));
            var expectedIds = new[]
            {
                EventTestData.ThirdEventId,
                EventTestData.VisitedEventId,
                EventTestData.FirstEventId,
            };
            var filterModel = new EventFilterViewModel
            {
                Order = EventOrderCriteria.RecentlyPublished,
            };

            var events = service.GetAll(filterModel, out _);
            var actualIds = events.Select(e => e.Id).ToArray();

            CollectionAssert.AreEqual(expectedIds, actualIds);
        }

        [Test]
        public void GetAllDraftEvents_ValidParameters_ReturnsAllDraftEvents()
        {
            const int pageNumber = 1;
            const int pageSize = 6;
            const int expectedCount = 3;

            service.GetAllDraftEvents(pageNumber, pageSize, out int actualCount);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        public async Task EditNextEvent_ValidEvent_SetsCorrectDateFrom(EventDto eventDto)
        {
            mockEventScheduleService.Setup(s => s.EventScheduleByEventId(It.IsAny<Guid>()))
                .Returns(EventTestHelpers.GetEventSchedule);
            eventDto.DateFrom = DateTime.Today.AddDays(7);

            var result = await service.EditNextEvent(eventDto);
            var test = await Context.Events.FindAsync(result);

            Assert.AreEqual(DateTime.Today.AddDays(7), test?.DateFrom);
            mockEventScheduleService.Verify(e => e.EventScheduleByEventId(It.IsAny<Guid>()), Times.Once);
            mockEventScheduleService.Verify(e => e.Edit(It.IsAny<EventScheduleDto>()), Times.Once);
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        public void Create_ValidEvent_ExecutesSuccessfully(EventDto eventDto)
        {
            var dto = EventTestHelpers.DeepCopyDto(eventDto);
            dto.Id = Guid.Empty;
            dto.Photo = EventTestHelpers.GetPhoto(@"./Images/valid-image.jpg");

            Assert.DoesNotThrowAsync(async () => await service.Create(dto));
            mockPhotoService.Verify(x => x.ChangeTempToImagePhoto(dto.Id), Times.Once);
        }

        [Test]
        [TestCaseSource(typeof(EditingOrCreatingExistingDto))]
        public void Edit_ValidEvent_ExecutesSuccessfully(EventDto eventDto)
        {
            MockMapper.Setup(m => m.Map<EventScheduleDto>(It.IsAny<EventDto>()))
                .Returns<EventDto>(EventTestHelpers.MapEventScheduleDtoFromEventDto);
            eventDto.Photo = EventTestHelpers.GetPhoto(@"./Images/valid-image.jpg");

            Assert.DoesNotThrowAsync(async () => await service.Edit(eventDto));
            mockPhotoService.Verify(x => x.ChangeTempToImagePhoto(eventDto.Id), Times.Once);
        }

        [Test]
        public void Edit_InvalidEvent_Throws()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.Edit(null));
        }

        [Test]
        [TestCaseSource(typeof(GetEventExistingId))]
        public void AddUserToEvent_ValidIds_ExecutesSuccessfully(Guid id)
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, id));
        }

        [Test]
        public void AddUserToEvent_IsPublicFalse_AssignsPendingStatus()
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, EventTestData.PrivateEventId));
            var ev = Context.UserEvent.First(ue => ue.EventId == EventTestData.PrivateEventId);

            Assert.AreEqual(ev.UserStatusEvent, UserStatusEvent.Pending);
        }

        [Test]
        public void AddUserToEvent_IsPublicNull_AssignsDeniedStatus()
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, EventTestData.IsPublicNullEventId));
            var ev = Context.UserEvent.First(ue => ue.EventId == EventTestData.IsPublicNullEventId);

            Assert.AreEqual(ev.UserStatusEvent, UserStatusEvent.Denied);
        }

        [Test]
        public void AddUserToEvent_UserNotFound_ThrowsWithAppropriateMessage()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.AddUserToEvent(Guid.NewGuid(), EventTestData.SecondEventId));

            Assert.That(ex?.Message, Is.Not.Null.And.Contains("User not found!"));
        }

        [Test]
        public void AddUserToEvent_TooMuchParticipants_ThrowsWithAppropriateMessage()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, EventTestData.VisitedEventId));

            Assert.That(ex?.Message, Is.Not.Null.And.Contains("Too much participants!"));
        }

        [Test]
        public void AddUserToEvent_EventNotFound_ThrowsWithAppropriateMessage()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, Guid.NewGuid()));

            Assert.That(ex?.Message, Is.Not.Null.And.Contains("Event not found!"));
        }

        [Test]
        public void AddUserToEvent_UnderageUserJoinsAgeRestrictedEvent_ThrowsWithAppropriateMessage()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.AddUserToEvent(EventTestData.SecondUserId, EventTestData.ThirdEventId));

            Assert.That(ex?.Message, Is.Not.Null.And.Contains("User does not meet age requirements!"));
        }

        [Test]
        public void AddUserToEvent_AdultUserJoinsAgeRestrictedEvent_ExecutesSuccessfully()
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, EventTestData.ThirdEventId));
        }

        [Test]
        public void AddUserToEvent_UserJoinsEventWithoutAgeRestriction_ExecutesSuccessfully()
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.AddUserToEvent(EventTestData.FirstUserId, EventTestData.FirstEventId));
        }

        [Test]
        public void DeleteUserFromEvent_ValidIds_ExecutesSuccessfully()
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.DeleteUserFromEvent(EventTestData.FirstUserId, EventTestData.VisitedEventId));
        }

        [Test]
        public void DeleteUserFromEvent_UserNotFound_Throws()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.DeleteUserFromEvent(Guid.NewGuid(), EventTestData.VisitedEventId));
        }

        [Test]
        public void DeleteUserFromEvent_EventNotFound_Throws()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.DeleteUserFromEvent(EventTestData.FirstUserId, Guid.NewGuid()));
        }

        [Test]
        public void ChangeVisitorStatus_ValidIds_ExecutesSuccessfully()
        {
            Assert.DoesNotThrowAsync(async () =>
                await service.ChangeVisitorStatus(EventTestData.FirstUserId, EventTestData.VisitedEventId, UserStatusEvent.Approved));
        }

        [Test]
        public void FutureEventsByUserId_ReturnEvents()
        {
            var events = service.FutureEventsByUserId(Guid.NewGuid(), EventTestData.PageModel);

            Assert.That(events, Is.Not.Null);
        }

        [Test]
        public void PastEventsByUserId_ReturnEvents()
        {
            var events = service.PastEventsByUserId(Guid.NewGuid(), EventTestData.PageModel);

            Assert.That(events, Is.Not.Null);
        }

        [Test]
        public void VisitedEventsByUserId_ReturnEvents()
        {
            var events = service.VisitedEventsByUserId(Guid.NewGuid(), EventTestData.PageModel);

            Assert.That(events, Is.Not.Null);
        }

        [Test]
        public void EventsToGoByUserId_ReturnEvents()
        {
            var events = service.EventsToGoByUserId(Guid.NewGuid(), EventTestData.PageModel);

            Assert.That(events, Is.Not.Null);
        }

        [Test]
        public void CreateDraft_Always_AddsNewEvent()
        {
            int eventCount = Context.Events.Count();

            service.CreateDraft();

            Assert.AreEqual(eventCount + 1, Context.Events.Count());
        }

        [Test]
        public void Publish_ValidEvent_SetsEventStatusToActive()
        {
            Assert.DoesNotThrowAsync(async () => await service.Publish(EventTestData.SecondEventId));
            var statusHistory = Context.Events.Find(EventTestData.SecondEventId)?.StatusHistory.Last();

            Assert.AreEqual(EventStatus.Active, statusHistory?.EventStatus);
        }

        [Test]
        public async Task CreateNextEvent_ValidEvent_SetsCorrectDateFrom()
        {
            MockMapper.Setup(m => m.Map<EventDto>(It.IsAny<Event>()))
                .Returns<Event>(EventTestHelpers.MapEventDtoFromEvent);
            mockEventScheduleService.Setup(s => s.EventScheduleByEventId(It.IsAny<Guid>()))
                .Returns(EventTestHelpers.GetEventSchedule);

            var result = await service.CreateNextEvent(EventTestData.FirstEventId);
            var test = await Context.Events.FindAsync(result);

            Assert.AreEqual(DateTime.Today.AddDays(7), test?.DateFrom);
        }

        [Test]
        public void GetEvents_ExistingIds_ReturnsExpectedEvents()
        {
            var paginationModel = EventTestData.PageModel;
            var eventIds = new List<Guid>
            {
                EventTestData.FirstEventId,
                EventTestData.SecondEventId,
            };

            service.GetEvents(eventIds, paginationModel);

            Assert.That(eventIds.Count, Is.EqualTo(paginationModel.Count));
        }

        [Test]
        public void GetEvents_NotExistingIds_ReturnsEmpty()
        {
            var eventIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            var result = service.GetEvents(eventIds, EventTestData.PageModel);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void SetRate_ValidIds_UpdatesExistingRate()
        {
            const byte score = 9;

            Assert.DoesNotThrowAsync(async () =>
                await service.SetRate(EventTestData.FirstUserId, EventTestData.VisitedEventId, score));
            var rate = Context.Events.Find(EventTestData.VisitedEventId)?.Rates.Last();

            Assert.AreEqual(score, rate?.Score);
        }

        [Test]
        public void SetRate_ValidIds_SetsNewRate()
        {
            const byte score = 9;

            Assert.DoesNotThrowAsync(async () =>
                await service.SetRate(EventTestData.FirstUserId, EventTestData.ThirdEventId, score));
            var rate = Context.Events.Find(EventTestData.ThirdEventId)?.Rates.Last();

            Assert.AreEqual(score, rate?.Score);
        }

        [Test]
        public void SetRate_InvalidEventId_Throws()
        {
            const byte score = 9;

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.SetRate(EventTestData.FirstUserId, Guid.NewGuid(), score));
        }

        [Test]
        public void GetRateFromUser_ValidIds_ReturnsCorrectScore()
        {
            byte expectedScore = EventTestData.Rates[0].Score;

            var result = service.GetRateFromUser(EventTestData.FirstUserId, EventTestData.VisitedEventId);

            Assert.AreEqual(expectedScore, result);
        }

        [Test]
        public void GetRateFromUser_InvalidIds_ReturnsZero()
        {
            var result = service.GetRateFromUser(Guid.NewGuid(), Guid.NewGuid());

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetRate_ValidEventId_ReturnsCorrectRate()
        {
            const double expectedAverageValue = 7;

            double actualRate = service.GetRate(EventTestData.VisitedEventId);

            Assert.AreEqual(expectedAverageValue, actualRate);
        }

        [Test]
        public void GetRate_InvalidEventId_ReturnsZero()
        {
            double rate = service.GetRate(Guid.NewGuid());

            Assert.AreEqual(0, rate);
        }

        [Test]
        public void UserIsVisitor_ValidUserId_ReturnsTrue()
        {
            bool result = service.UserIsVisitor(EventTestData.FirstUserId, EventTestData.VisitedEventId);

            Assert.IsTrue(result);
        }

        [Test]
        public void UserIsVisitor_InvalidUserId_ReturnsFalse()
        {
            bool result = service.UserIsVisitor(Guid.NewGuid(), EventTestData.VisitedEventId);

            Assert.IsFalse(result);
        }

        [Test]
        public void UserIsVisitor_InvalidEventId_ReturnsFalse()
        {
            bool result = service.UserIsVisitor(EventTestData.FirstUserId, Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public void Exists_ExistingId_ReturnsTrue()
        {
            bool result = service.Exists(EventTestData.FirstEventId);

            Assert.IsTrue(result);
        }

        [Test]
        public void Exists_NotExistingEvent_ReturnsFalse()
        {
            bool result = service.Exists(Guid.NewGuid());

            Assert.IsFalse(result);
        }
    }
}
