using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventScheduleServiceTests : TestInitializer
    {
        private static Mock<ISecurityContext> mockSecurityContextService;
        private EventScheduleService service;
        private List<EventSchedule> eventSchedules;
        private EventScheduleDto esDTO;
        private Event activeEvent;
        private Event draftEvent;

        private Guid validEventScheduleId = Guid.NewGuid();
        private Guid todayEventScheduleId = Guid.NewGuid();
        private Guid validEventId = Guid.NewGuid();
        private Guid validUserId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockSecurityContextService = new Mock<ISecurityContext>();

            service = new EventScheduleService(
                Context,
                MockMapper.Object,
                mockSecurityContextService.Object);

            activeEvent = new Event
            {
                Id = validEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today.AddDays(2),
                Description = "...",
                Organizers = new List<EventOrganizer>()
                {
                    new EventOrganizer { UserId = validUserId },
                },
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory { EventStatus = EventStatus.Active },
                },
            };
            draftEvent = new Event
            {
                Id = Guid.NewGuid(),
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today.AddDays(2),
                Description = "...",
                Organizers = new List<EventOrganizer>()
                {
                    new EventOrganizer { UserId = validUserId },
                },
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory { EventStatus = EventStatus.Draft },
                },
            };

            eventSchedules = new List<EventSchedule>
            {
                new EventSchedule
                {
                    Id = validEventScheduleId,
                    Frequency = 2,
                    Periodicity = Periodicity.Daily,
                    LastRun = new DateTime(2020, 12, 08),
                    NextRun = new DateTime(2020, 12, 10),
                    IsActive = true,
                    Event = activeEvent,
                    EventId = validEventId,
                },

                new EventSchedule
                {
                    Id = todayEventScheduleId,
                    Frequency = 2,
                    Periodicity = Periodicity.Daily,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(2),
                    IsActive = true,
                    Event = draftEvent,
                    EventId = activeEvent.Id,
                },
            };

            esDTO = new EventScheduleDto
            {
                    Id = validEventScheduleId,
                    Frequency = 1,
                    Periodicity = Periodicity.Yearly,
                    LastRun = new DateTime(2020, 11, 25),
                    NextRun = new DateTime(2021, 11, 25),
                    IsActive = true,
                    EventId = validEventId,
            };

            Context.Events.Add(activeEvent);
            Context.Events.Add(draftEvent);
            Context.EventSchedules.AddRange(eventSchedules);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<EventSchedule, EventScheduleDto>(It.IsAny<EventSchedule>()))
                .Returns((EventSchedule e) => e == null ?
                null :
                new EventScheduleDto
                {
                    Id = e.Id,
                    Frequency = e.Frequency,
                    Periodicity = e.Periodicity,
                    LastRun = e.LastRun,
                    NextRun = e.NextRun,
                    IsActive = e.IsActive,
                    EventId = e.EventId,
                });
            MockMapper.Setup(u => u.Map<IEnumerable<EventScheduleDto>>(It.IsAny<IEnumerable<EventSchedule>>()))
                .Returns((IEnumerable<EventSchedule> e) => e?.Select(item => new EventScheduleDto { Id = item.Id }));
        }

        [Test]
        public void GetAll_Works()
        {
            mockSecurityContextService.Setup(x => x.GetCurrentUserId()).Returns(validUserId);

            var result = service.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            mockSecurityContextService.Verify(x => x.GetCurrentUserId(), Times.Once);
        }

        [Test]
        public void GetEventSchedule_ExistingId_ReturnTrue()
        {
            var result = service.EventScheduleById(validEventScheduleId);

            Assert.AreEqual(eventSchedules[0].Id, result.Id);
        }

        [Test]
        public void GetEventSchedule_NotExistingId_ReturnNull()
        {
            var result = service.EventScheduleById(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetEventScheduleByEvent_ExistingId_ReturnTrue()
        {
            var result = service.EventScheduleByEventId(validEventId);

            Assert.AreEqual(eventSchedules[0].Id, result.Id);
        }

        [Test]
        public void GetEventScheduleByEvent_NotExistingId_ReturnNull()
        {
            var result = service.EventScheduleById(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetUrgentEventSchedules_Works()
        {
            var result = service.GetUrgentEventSchedules();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void Create_newEventSchedule_Success()
        {
            MockMapper.Setup(u => u.Map<EventScheduleDto, EventSchedule>(It.IsAny<EventScheduleDto>()))
                .Returns((EventScheduleDto e) => e == null ?
                null :
                new EventSchedule
                {
                    Id = e.Id,
                    Frequency = e.Frequency,
                    Periodicity = e.Periodicity,
                    LastRun = e.LastRun,
                    NextRun = e.NextRun,
                    IsActive = e.IsActive,
                    EventId = e.EventId,
                });

            esDTO.Id = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await service.Create(esDTO));
        }

        [Test]
        public void Edit_EventSchedule_Success()
        {
            Assert.DoesNotThrowAsync(async () => await service.Edit(esDTO));
        }

        [Test]
        public void Edit_EventSchedule_NotFound_ThrouAsync()
        {
            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(new EventScheduleDto()));
            Assert.That(ex.Message, Contains.Substring("Not found"));
        }

        [Test]
        [Category("Delete event")]
        public void DeleteEventSchedule_ExistingId_Success()
        {
            Assert.DoesNotThrowAsync(async () => await service.Delete(validEventScheduleId));
        }

        [Test]
        [Category("Delete event")]
        public async Task DeleteEventSchedule_ExistingId_ReturnsCorrectId()
        {
            var result = await service.Delete(validEventScheduleId);

            Assert.AreEqual(validEventScheduleId, result);
            var testEventSchedule = Context.EventSchedules.Find(result);
            Assert.IsNull(testEventSchedule);
        }

        [Test]
        [Category("Delete event")]
        public void DeleteEventSchedule_NotExistingId_ThrowsAsync()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Delete(Guid.NewGuid()));
        }

        [Test]
        [Category("Cancel events")]
        public void CancelEvents_WorksAsync()
        {
            Assert.DoesNotThrowAsync(async () => await service.CancelEvents(validEventId));
        }

        [Test]
        [Category("Cancel events")]
        public async Task CancelEvents_ScheduleIsInactive_Success()
        {
            var result = await service.CancelEvents(validEventId);

            Assert.AreEqual(validEventScheduleId, result);
            var changedEventSchedule = Context.EventSchedules.Find(result);
            Assert.AreEqual(false, changedEventSchedule.IsActive);
        }

        [Test]
        [Category("Cancel next event")]
        public void CancelNextEvent_WorksAsync()
        {
            Assert.DoesNotThrowAsync(async () => await service.CancelNextEvent(validEventId));
        }

        [Test]
        [Category("Cancel next event")]
        public async Task CancelNextEvent_NewScheduleRuns_Success()
        {
            var expectedLastRun = eventSchedules[0].LastRun.AddDays(2);
            var expectedNextRun = eventSchedules[0].NextRun.AddDays(2);

            var result = await service.CancelNextEvent(validEventId);

            Assert.AreEqual(validEventScheduleId, result);
            var actualEventSchedule = Context.EventSchedules.Find(result);
            Assert.AreEqual(expectedLastRun, actualEventSchedule.LastRun);
            Assert.AreEqual(expectedNextRun, actualEventSchedule.NextRun);
        }
    }
}
