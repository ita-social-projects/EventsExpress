using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Test.ServiceTests.TestClasses.Location;
using Moq;
using NUnit.Framework;
using Location = EventsExpress.Db.Entities.Location;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class LocationManagerTest : TestInitializer
    {
        private ILocationManager service;
        private Location locationMap;
        private Location locationOnline;

        private Location FromDtoToEf(LocationDto locationDto)
        {
            return new Location
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
            service = new LocationManager(Context, MockMapper.Object);
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

            Assert.That(Guid.Equals(result, locationDto.Id), Is.True);
        }

        [TestCaseSource(typeof(CreatingNotExistingLocation))]
        [Category("Create")]
        public void Create_newLocation_DoesNotThrowAsync(LocationDto locationDto)
        {
            Assert.DoesNotThrow(() => service.Create(locationDto));
        }

        [TestCaseSource(typeof(CreatingExistingLocation))]
        [Category("Create")]
        public void Create_ExistingLocation_Failed(LocationDto locationDto)
        {
            Assert.Throws<InvalidOperationException>(
                 () => service.Create(locationDto));
        }

        [TestCaseSource(typeof(CreatingExistingLocation))]
        [Category("EditLocation")]
        public void EditLocation_ExistingLocation_ReturnExistingLocationId(LocationDto locationDto)
        {
            Assert.Throws<InvalidOperationException>(
                () => service.EditLocation(locationDto));
        }

        [TestCaseSource(typeof(CreatingExistingLocation))]
        [Category("Edit Location")]
        public void EditLocation_NotExistingLocation_CreateNewLocation(LocationDto locationDto)
        {
            Assert.Throws<InvalidOperationException>(
                () => service.EditLocation(locationDto));
        }
    }
}
