using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class EventStatusHistoryControllerTest
    {
        private Mock<IEventStatusHistoryService> service;
        private EventStatusHistoryController controller;

        private EventStatusHistoryViewModel firstEventStatusDTO;
        private EventStatusHistoryViewModel secondEventStatusDTO;
        private EventStatusHistoryViewModel thirdEventStatusDTO;
        private Guid eventId = Guid.NewGuid();
        private string reason = "testReason";
        private EventStatusHistoryViewModel eventStatus;
        private EventStatus evStatus = EventStatus.Blocked;

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            service = new Mock<IEventStatusHistoryService>();
            controller = new EventStatusHistoryController(service.Object);
            firstEventStatusDTO = new EventStatusHistoryViewModel
            {
                EventId = eventId,
                Reason = reason,
                EventStatus = EventStatus.Active,
            };
            secondEventStatusDTO = new EventStatusHistoryViewModel
            {
                EventId = eventId,
                Reason = reason,
                EventStatus = EventStatus.Blocked,
            };
            thirdEventStatusDTO = new EventStatusHistoryViewModel
            {
                EventId = eventId,
                Reason = reason,
                EventStatus = EventStatus.Canceled,
            };
        }

        [Test]
        public void SetStatus_OkResult()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<EventStatusHistoryViewModel>>(It.IsAny<IEnumerable<EventStatusHistoryViewModel>>()))
            .Returns((IEnumerable<EventStatusHistoryViewModel> e) => e.Select(item => new EventStatusHistoryViewModel { EventId = item.EventId, Reason = item.Reason, EventStatus = item.EventStatus }));
            Moq.Language.Flow.IReturnsResult<IEventStatusHistoryService> returnsResult = service.Setup(item => item.SetStatusEvent(eventId, reason, evStatus)).Returns(new EventStatusHistoryViewModel[] { firstEventStatusDTO, secondEventStatusDTO, thirdEventStatusDTO });

            var expected = controller.SetStatus(eventId, eventStatus);

            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
