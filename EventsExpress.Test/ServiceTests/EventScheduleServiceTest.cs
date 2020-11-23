using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using MediatR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventScheduleServiceTest : TestInitializer
    {
        private static Mock<IMediator> mockMediator;
        private EventScheduleService service;
        private List<EventSchedule> eventSchedules;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();

            service = new EventScheduleService(
                MockUnitOfWork.Object,
                MockMapper.Object,
                mockMediator.Object);

            eventSchedules = new List<EventSchedule>
            {
                new EventSchedule
                {
                    Id = new Guid("9A69B4F7-612F-4241-2B74-08D88EC1DBDA"),
                    CreatedBy = new Guid("DFA88411-F08A-4BCA-57CC-08D87784DED8"),
                    CreatedDateTime = new DateTime(2020, 11, 20),
                    ModifiedBy = new Guid("DFA88411-F08A-4BCA-57CC-08D87784DED8"),
                    ModifiedDateTime = new DateTime(2020, 11, 23),
                    Frequency = 2,
                    Periodicity = Db.Enums.Periodicity.Daily,
                    LastRun = new DateTime(2020, 12, 10),
                    NextRun = new DateTime(2020, 12, 12),
                    IsActive = true,
                    EventId = new Guid("5644A548-E4E5-43B5-1254-08D88EC1DBD1"),
                },

                new EventSchedule
                {
                    Id = new Guid("7F8FBD8C-C472-48A2-2B75-08D88EC1DBDA"),
                    CreatedBy = new Guid("DFA88411-F08A-4BCA-A860-E5A2664B013D"),
                    CreatedDateTime = new DateTime(2020, 10, 12),
                    ModifiedBy = new Guid("DFA88411-F08A-4BCA-A860-E5A2664B013D"),
                    ModifiedDateTime = new DateTime(2020, 10, 12),
                    Frequency = 1,
                    Periodicity = Db.Enums.Periodicity.Yearly,
                    LastRun = new DateTime(2020, 11, 28),
                    NextRun = new DateTime(2021, 11, 28),
                    IsActive = true,
                    EventId = new Guid("D5E058D2-196C-4DED-1256-08D88EC1DBD1"),
                },
            };

            List<Event> events = new List<Event>
            {
                new Event
                {
                    Id = new Guid("5644A548-E4E5-43B5-1254-08D88EC1DBD1"),
                    CityId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "description...",
                    OwnerId = new Guid("DFA88411-F08A-4BCA-57CC-08D87784DED8"),
                    PhotoId = new Guid("62FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "Miami",
                    IsBlocked = false,
                    Categories = null,
                },
                new Event
                {
                    Id = new Guid("32FA643C-AD14-5BCC-A860-E5A2664B019D"),
                    CityId = new Guid("31FA647C-AD54-4BCC-A860-E5A2664B019D"),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl fgr sdmkskdl dsnlndsl",
                    OwnerId = new Guid("34FA647C-AD54-2BCC-A860-E5A2664B013D"),
                    PhotoId = new Guid("11FA647C-AD54-4BCC-A860-E5A2261B019D"),
                    Title = "SLdndstrhndj",
                    IsBlocked = false,
                    Categories = null,
                    Visitors = null,
                },
            };

            MockUnitOfWork.Setup(u => u.EventScheduleRepository
                .Delete(It.IsAny<EventSchedule>())).Returns((Event i) => eventSchedules.Where(x => x.Id == i.Id).FirstOrDefault());

            MockUnitOfWork.Setup(u => u.EventScheduleRepository
                .Get(It.IsAny<Guid>())).Returns((Guid i) => eventSchedules.Where(x => x.Id == i).FirstOrDefault());
        }

        [Test]
        public void GetEventSchedule_IdExists_ReturnTrue()
        {
            var result = service.EventScheduleById(new Guid("9A69B4F7-612F-4241-2B74-08D88EC1DBDA"));

            Assert.AreEqual(eventSchedules[0].Id, result.Id);
        }
    }
}
