using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class TrackMapperProfileTests : MapperTestInitializer<TrackMapperProfile>
    {
        private ChangeInfo _firstTrack;
        private TrackDto _firstTrackDto;

        private ChangeInfo GetTrack()
        {
            return new ChangeInfo { EntityName = "Event", ChangesType = ChangesType.Create, Time = DateTime.Today };
        }

        private TrackDto GetTrackDto()
        {
            return new TrackDto { Name = "Event", ChangesType = ChangesType.Create, Time = DateTime.Today };
        }

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void TrackMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void TrackMapperProfile_ChangeInfoToTrackDto()
        {
            _firstTrack = GetTrack();
            var resEven = Mapper.Map<ChangeInfo, TrackDto>(_firstTrack);

            Assert.That(resEven.Name, Is.EqualTo(_firstTrack.EntityName));
        }

        [Test]
        public void TrackMapperProfile_TrackDtoToTrackViewModel()
        {
            _firstTrackDto = GetTrackDto();
            var resEven = Mapper.Map<TrackDto, TrackViewModel>(_firstTrackDto);

            Assert.That(resEven.EntityName, Is.EqualTo(_firstTrackDto.Name));
        }
    }
}
