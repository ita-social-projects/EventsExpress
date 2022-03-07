using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Test.ServiceTests.TestClasses.Location;
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
        private Db.Entities.Location locationMap;
        private Db.Entities.Location locationOnline;

        private Db.Entities.Location FromDtoToEf(LocationDto locationDto)
        {
            return new Db.Entities.Location
            {
                Id = locationDto.Id,
                Point = locationDto.Point,
                OnlineMeeting = locationDto.OnlineMeeting,
                Type = locationDto.Type,
            };
        }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new LocationService(Context, MockMapper.Object);
            locationMap = FromDtoToEf(CreatingExistingLocation.LocationDTOMap);
            locationOnline = FromDtoToEf(CreatingExistingLocation.LocationDTOOnline);

            Context.Locations.Add(locationMap);
            Context.SaveChanges();
            Context.Locations.Add(locationOnline);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<LocationDto>(It.IsAny<Db.Entities.Location>()))
                .Returns((Db.Entities.Location el) => el == null ?
                null :
                new LocationDto
                {
                    Id = el.Id,
                    Point = el.Point,
                    OnlineMeeting = el.OnlineMeeting,
                    Type = el.Type,
                });

            MockMapper.Setup(u => u.Map<LocationDto, Db.Entities.Location>(It.IsAny<LocationDto>()))
                .Returns((LocationDto el) => el == null ?
                null :
                new Db.Entities.Location
                {
                    Id = el.Id,
                    Point = el.Point,
                    OnlineMeeting = el.OnlineMeeting,
                    Type = el.Type,
                });
        }

        [TestCaseSource(typeof(CreatingNotExistingLocation))]
        [Category("Create")]
        public void Create_newLocation_IdEquals(LocationDto locationDto)
        {
            var result = service.Create(locationDto);

            Assert.That(Guid.Equals(result.Result, locationDto.Id), Is.True);
        }

        [TestCaseSource(typeof(CreatingNotExistingLocation))]
        [Category("Create")]
        public void Create_newLocation_DoesNotThrowAsync(LocationDto locationDto)
        {
            Assert.DoesNotThrowAsync(
                async () => await service.Create(locationDto));
        }

        [TestCaseSource(typeof(CreatingExistingLocation))]
        [Category("Create")]
        public void Create_ExistingLocation_Failed(LocationDto locationDto)
        {
            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await service.Create(locationDto));
        }

        [Test]
        [Category("Location by Point")]
        public void LocationByPoint_ExistingPoint_ReturnLocationDTO()
        {
            Point point = new Point(locationMap.Point.X, locationMap.Point.Y);
            Guid id = locationMap.Id;

            var actual = service.LocationByPoint(point);

            Assert.That(Is.Equals(actual.Id, id), Is.True);
        }

        [Test]
        [Category("Location by Point")]
        public void LocationByPoint_NotExistingPoint_ReturnLocationDTO()
        {
            Point point = new Point(1.1, 1.8);

            var actual = service.LocationByPoint(point);

            Assert.That(actual, Is.Null);
        }

        [Test]
        [Category("Location by Url")]
        public void LocationByUrl_ExistingUrl_ReturnLocationDTO()
        {
            string uri = locationOnline.OnlineMeeting;
            Guid id = locationOnline.Id;

            var actual = service.LocationByURI(uri);

            Assert.That(Is.Equals(actual.Id, id), Is.True);
        }

        [Test]
        [Category("Location by Url")]
        public void LocationByUrl_NotExistingUrl_ReturnLocationDTO()
        {
            string uri = "http://basin.example.com/#branch";

            var actual = service.LocationByURI(uri);

            Assert.That(actual, Is.Null);
        }

        [TestCaseSource(typeof(CreatingExistingLocation))]
        [Category("Add Location To Event")]
        public void AddLocationToEvent_ExistingLocation_ReturnExistingLocationId(LocationDto locationDto)
        {
            var actual = service.AddLocationToEvent(locationDto);
            Assert.That(Is.Equals(locationDto.Id, actual.Result), Is.True);
        }

        [TestCaseSource(typeof(CreatingNotExistingLocation))]
        [Category("Add Location To Event")]
        public void AddLocationToEvent_NotExistingLocation_CreateNewLocation(LocationDto locationDto)
        {
            Assert.DoesNotThrowAsync(
                async () => await service.AddLocationToEvent(locationDto));
        }
    }
}
