using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using MediatR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventScheduleServiceTests : TestInitializer
    {
        private static Mock<IMediator> mockMediator;
        private EventScheduleService service;
        private List<EventSchedule> eventSchedules;
        private EventScheduleDTO esDTO;
        private Event evnt;
        private Photo photo;
        private City city;
        private Country country;

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

            service = new EventScheduleService(
                Context,
                MockMapper.Object);

            country = new Country()
            {
                Id = validCountryId,
                Name = "Country",
            };

            city = new City()
            {
                Id = validCityId,
                Name = "City",
                CountryId = validCountryId,
                Country = country,
            };

            photo = new Photo
            {
                Id = validPhotoId,
                Thumb = new byte[0],
                Img = new byte[0],
            };

            evnt = new Event
            {
                Id = validEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "...",
                PhotoId = validPhotoId,
                Photo = photo,
                CityId = validCityId,
                City = city,
            };

            eventSchedules = new List<EventSchedule>
            {
                new EventSchedule
                {
                    Id = validEventScheduleId,
                    CreatedBy = validUserId,
                    CreatedDateTime = new DateTime(2020, 11, 20),
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
                    CreatedBy = validUserId,
                    CreatedDateTime = new DateTime(2020, 11, 20),
                    Frequency = 2,
                    Periodicity = Db.Enums.Periodicity.Daily,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(2),
                    IsActive = true,
                    EventId = Guid.NewGuid(),
                },
            };

            esDTO = new EventScheduleDTO
            {
                    Id = validEventScheduleId,
                    CreatedBy = Guid.NewGuid(),
                    CreatedDateTime = new DateTime(2020, 10, 12),
                    ModifiedBy = Guid.NewGuid(),
                    ModifiedDateTime = new DateTime(2020, 10, 12),
                    Frequency = 1,
                    Periodicity = Db.Enums.Periodicity.Yearly,
                    LastRun = new DateTime(2020, 11, 25),
                    NextRun = new DateTime(2021, 11, 25),
                    IsActive = true,
                    EventId = validEventId,
            };

            Context.Photos.Add(photo);
            Context.Countries.Add(country);
            Context.Cities.Add(city);
            Context.Events.Add(evnt);
            Context.EventSchedules.AddRange(eventSchedules);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<EventSchedule, EventScheduleDTO>(It.IsAny<EventSchedule>()))
                .Returns((EventSchedule e) => e == null ?
                null :
                new EventScheduleDTO
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
            MockMapper.Setup(u => u.Map<EventScheduleDTO, EventSchedule>(It.IsAny<EventScheduleDTO>()))
                .Returns((EventScheduleDTO e) => e == null ?
                null :
                new EventSchedule
                {
                    Id = e.Id,
                    CreatedBy = e.CreatedBy,
                    CreatedDateTime = e.CreatedDateTime,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDateTime = e.ModifiedDateTime,
                    Frequency = e.Frequency,
                    Periodicity = e.Periodicity,
                    LastRun = e.LastRun,
                    NextRun = e.NextRun,
                    IsActive = e.IsActive,
                    EventId = e.EventId,
                });

            esDTO.Id = Guid.NewGuid();

            // Act
            var res = service.Create(esDTO);

            // Assert
            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Edit_EventSchedule_Success()
        {
            // Arrange

            // Act
            var res = service.Edit(esDTO);

            // Assert
            Assert.IsTrue(res.Result.Successed);
        }
    }
}
