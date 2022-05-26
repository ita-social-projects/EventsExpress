using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class EventScheduleMapperProfileTests : MapperTestInitializer<EventScheduleMapperProfile>
    {
        private EventScheduleDto eventScheduleDto = new EventScheduleDto
        {
            Id = Guid.NewGuid(),
            Frequency = 0,
            Periodicity = Db.Enums.Periodicity.Daily,
            LastRun = new DateTime(2021, 01, 01),
            NextRun = new DateTime(2021, 02, 01),
            IsActive = true,
            EventId = Guid.NewGuid(),
            Event = new EventDto
            {
                Id = Guid.NewGuid(),
            },
        };

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void EventScheduleAutoMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void EventScheduleMapperProfile_EventScheduleDtoToEventScheduleViewModel()
        {
            var eventScheduleViewModel = Mapper.Map<EventScheduleDto, EventScheduleViewModel>(eventScheduleDto);

            Assert.That(eventScheduleViewModel.Id, Is.EqualTo(eventScheduleDto.Id));
            Assert.That(eventScheduleViewModel.Frequency, Is.EqualTo(eventScheduleDto.Frequency));
            Assert.That(eventScheduleViewModel.Periodicity, Is.EqualTo(eventScheduleDto.Periodicity));
            Assert.That(eventScheduleViewModel.EventId, Is.EqualTo(eventScheduleDto.EventId));
            Assert.That(eventScheduleViewModel.LastRun, Is.EqualTo(eventScheduleDto.LastRun));
            Assert.That(eventScheduleViewModel.NextRun, Is.EqualTo(eventScheduleDto.NextRun));
            Assert.That(eventScheduleViewModel.Title, Is.EqualTo(eventScheduleDto.Event.Title));
            Assert.That(eventScheduleViewModel.IsActive, Is.EqualTo(eventScheduleDto.IsActive));
            Assert.That(eventScheduleViewModel.Organizers, Has.All.Matches<UserPreviewViewModel>(x => eventScheduleDto.Event.Organizers
                .All(o => x.Id == o.Id && x.FirstName == o.FirstName)));
        }

        [Test]
        public void EventScheduleMapperProfile_EventScheduleDtoToPreviewEventScheduleViewModel()
        {
            var eventScheduleViewModel = Mapper.Map<EventScheduleDto, PreviewEventScheduleViewModel>(eventScheduleDto);

            Assert.That(eventScheduleViewModel.Id, Is.EqualTo(eventScheduleDto.Id));
            Assert.That(eventScheduleViewModel.Frequency, Is.EqualTo(eventScheduleDto.Frequency));
            Assert.That(eventScheduleViewModel.Periodicity, Is.EqualTo(eventScheduleDto.Periodicity));
            Assert.That(eventScheduleViewModel.EventId, Is.EqualTo(eventScheduleDto.EventId));
            Assert.That(eventScheduleViewModel.LastRun, Is.EqualTo(eventScheduleDto.LastRun));
            Assert.That(eventScheduleViewModel.NextRun, Is.EqualTo(eventScheduleDto.NextRun));
            Assert.That(eventScheduleViewModel.Title, Is.EqualTo(eventScheduleDto.Event.Title));
            Assert.That(eventScheduleViewModel.IsActive, Is.EqualTo(eventScheduleDto.IsActive));
        }
    }
}
