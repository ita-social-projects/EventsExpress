using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventStatusHistoryServiceTest : TestInitializer
    {
        private static Mock<ISecurityContext> mockSecurityContextService;

        private EventStatusHistoryService service;
        private Guid eventId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();
        private string reason = "testReason";
        private EventStatus eventStatus = EventStatus.Active;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockSecurityContextService = new Mock<ISecurityContext>();
            mockSecurityContextService.Setup(x => x.GetCurrentUserId())
                .Returns(userId);

            service = new EventStatusHistoryService(
                Context,
                mockSecurityContextService.Object);
        }

        [Test]
        [Category("Set status event")]
        public void SetStatusEvent_InvalidEventId_ThrowsException()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.SetStatusEvent(eventId, reason, eventStatus));
        }

        [Test]
        [Category("Set status event")]
        public async Task SetStatusEvent_InsertEventStatus_ReturnTrue()
        {
             Context.Events.Add(new Event { Id = eventId });
             Context.SaveChanges();
             await service.SetStatusEvent(eventId, reason, eventStatus);
             var res = Context.EventStatusHistory.LastOrDefault(e => e.EventId == eventId && e.EventStatus == eventStatus);
             Assert.IsNotNull(res);
        }
    }
}
