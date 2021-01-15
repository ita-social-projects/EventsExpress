using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class LocationServiceTest : TestInitializer
    {
        private LocationService service;
        private EventLocation location;
        private LocationDto locationDTO;

        private Guid locationId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new LocationService(Context, MockMapper.Object);
            location = new EventLocation
            {
                Id = locationId,
                Point = new Point(12.45, 24.6),
            };

            locationDTO = new LocationDto
            {
                Id = locationId,
                Point = new Point(12.45, 24.6),
            };

            Context.EventLocations.Add(location);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<LocationDto>(It.IsAny<EventLocation>()))
                .Returns((EventLocation el) => el == null ?
                null :
                new LocationDto
                {
                    Id = el.Id,
                    Point = el.Point,
                });

            MockMapper.Setup(u => u.Map<LocationDto, EventLocation>(It.IsAny<LocationDto>()))
                .Returns((LocationDto el) => el == null ?
                null :
                new EventLocation
                {
                    Id = el.Id,
                    Point = el.Point,
                });
        }

        [Test]
        public void Create_newLocation_Success()
        {
            // Arrange
            locationDTO.Id = Guid.NewGuid();

            // Act

            // Assert
            Assert.DoesNotThrowAsync(
                async () => await service.Create(locationDTO));
        }

        [Test]
        public void Create_ExistingLocation_Failed()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await service.Create(locationDTO));
        }

        [Test]
        public void LocationByPoint_ExistingPoint_ReturnLocationDTO()
        {
            // Arrange

            // Act
            var actual = service.LocationByPoint(locationDTO.Point);

            // Assert
            Assert.AreEqual(locationDTO.Id, actual.Id);
        }

        [Test]
        public void LocationByPoint_NotExistingPoint_ReturnNull()
        {
            // Arrange

            // Act
            var actual = service.LocationByPoint(new Point(12.45, 24.00));

            // Assert
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void AddLocationToEvent_ExistingLocation_ReturnExistingLocationId()
        {
            // Arrange

            // Act
            var actual = service.AddLocationToEvent(locationDTO);

            // Assert
            Assert.AreEqual(locationDTO.Id, actual.Result);
        }

        [Test]
        public void AddLocationToEvent_NotExistingLocation_CreateNewLocation()
        {
            // Arrange
            var locationDTO = new LocationDto { Point = new Point(10.23, 2.3) };

            // Act

            // Assert
            Assert.DoesNotThrowAsync(
                async () => await service.AddLocationToEvent(locationDTO));
        }
    }
}
