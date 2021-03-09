using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
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

        private Guid eventId = Guid.NewGuid();
        private string reason = "testReason";
        private EventStatusHistoryViewModel eventStatus;
        private EventStatus evStatus = EventStatus.Blocked;

        [SetUp]
        protected void Initialize()
        {
            service = new Mock<IEventStatusHistoryService>();
            controller = new EventStatusHistoryController(service.Object);
            eventStatus = new EventStatusHistoryViewModel { EventId = eventId, EventStatus = EventStatus.Blocked };
        }

        [Test]
        public async Task SetStatus_OkResult()
        {
            service.Setup(item => item.SetStatusEvent(eventId, reason, evStatus)).Returns(Task.CompletedTask);

            var expected = await controller.SetStatus(eventId, eventStatus);
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
