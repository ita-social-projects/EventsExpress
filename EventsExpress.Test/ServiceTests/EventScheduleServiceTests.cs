using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventScheduleServiceTests : TestInitializer
    {
        private static Mock<IMediator> mockMediator;
        private static Mock<IHttpContextAccessor> httpContextAccessor;
        private static Mock<IAuthService> authService;
        private EventScheduleService service;
        private List<EventSchedule> eventSchedules;
        private EventScheduleDto esDTO;
        private Event evnt;

        private Guid validEventScheduleId = Guid.NewGuid();
        private Guid todayEventScheduleId = Guid.NewGuid();
        private Guid validEventId = Guid.NewGuid();
        private Guid validUserId = Guid.NewGuid();
        private Guid validPhotoId = Guid.NewGuid();
        private Guid validCityId = Guid.NewGuid();
        private Guid validCountryId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            authService = new Mock<IAuthService>();

            service = new EventScheduleService(
                Context,
                MockMapper.Object,
                authService.Object,
                httpContextAccessor.Object);

            evnt = new Event
            {
                Id = validEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "...",
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory { EventStatus = EventStatus.Active },
                },
            };

            eventSchedules = new List<EventSchedule>
            {
                new EventSchedule
                {
                    Id = validEventScheduleId,
                    Frequency = 2,
                    Periodicity = Db.Enums.Periodicity.Daily,
                    LastRun = new DateTime(2020, 12, 08),
                    NextRun = new DateTime(2020, 12, 10),
                    IsActive = true,
                    Event = evnt,
                    EventId = validEventId,
                },

                new EventSchedule
                {
                    Id = todayEventScheduleId,
                    Frequency = 2,
                    Periodicity = Db.Enums.Periodicity.Daily,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(2),
                    IsActive = true,
                    EventId = Guid.NewGuid(),
                },
            };

            esDTO = new EventScheduleDto
            {
                    Id = validEventScheduleId,
                    Frequency = 1,
                    Periodicity = Db.Enums.Periodicity.Yearly,
                    LastRun = new DateTime(2020, 11, 25),
                    NextRun = new DateTime(2021, 11, 25),
                    IsActive = true,
                    EventId = validEventId,
            };

            Context.Events.Add(evnt);
            Context.EventSchedules.AddRange(eventSchedules);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<EventSchedule, EventScheduleDto>(It.IsAny<EventSchedule>()))
                .Returns((EventSchedule e) => e == null ?
                null :
                new EventScheduleDto
                {
                    Id = e.Id,
                    EventId = e.EventId,
                });
        }

        [Test]
        public void GetEventSchedule_ExistingId_ReturnTrue()
        {
            // Arrange

            // Act
            var result = service.EventScheduleById(validEventScheduleId);

            // Assert
            Assert.AreEqual(eventSchedules[0].Id, result.Id);
        }

        [Test]
        public void GetEventSchedule_NotExistingId_ReturnNull()
        {
            // Arrange

            // Act
            var result = service.EventScheduleById(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetEventScheduleByEvent_ExistingId_ReturnTrue()
        {
            // Arrange

            // Act
            var result = service.EventScheduleByEventId(validEventId);

            // Assert
            Assert.AreEqual(eventSchedules[0].Id, result.Id);
        }

        [Test]
        public void GetEventScheduleByEvent_NotExistingId_ReturnNull()
        {
            // Arrange

            // Act
            var result = service.EventScheduleById(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_newEventSchedule_Success()
        {
            // Arrange
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

            // Act
            // Assert
            Assert.DoesNotThrowAsync(async () => await service.Create(esDTO));
        }

        [Test]
        public void Edit_EventSchedule_Success()
        {
            // Arrange

            // Act
            // Assert
            Assert.DoesNotThrowAsync(async () => await service.Edit(esDTO));
        }
    }
}
