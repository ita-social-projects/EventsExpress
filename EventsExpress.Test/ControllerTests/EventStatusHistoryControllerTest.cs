using System;
using System.Threading.Tasks;
using EventsExpress.Controllers;
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
        private EventStatusHistoryViewModel eventStatus;

        [SetUp]
        protected void Initialize()
        {
            service = new Mock<IEventStatusHistoryService>();
            controller = new EventStatusHistoryController(service.Object);
            eventStatus = new EventStatusHistoryViewModel { EventId = eventId, Reason = "test", EventStatus = EventStatus.Blocked };
        }

        [Test]
        public async Task SetStatus_OkResult()
        {
            service.Setup(item => item.SetStatusEvent(eventStatus.EventId, eventStatus.Reason, eventStatus.EventStatus)).Returns(Task.CompletedTask);

            var expected = await controller.SetStatus(eventId, eventStatus);
            Assert.DoesNotThrowAsync(() => Task.FromResult(expected));
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void SetStatus_ThrowsException()
        {
            service.Setup(item => item.SetStatusEvent(eventStatus.EventId, eventStatus.Reason, eventStatus.EventStatus)).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => controller.SetStatus(eventId, eventStatus));
        }
    }
}
