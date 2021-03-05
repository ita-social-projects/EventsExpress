using System;
using System.Security.Claims;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class EventStatusHistoryServiceTest : TestInitializer
    {
        private static Mock<IMediator> mockMediator;
        private static Mock<IAuthService> mockAuthService;
        private static Mock<IHttpContextAccessor> httpContextAccessor;

        private EventStatusHistoryService service;
        private Guid eventId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();
        private string reason = "testReason";
        private EventStatus eventStatus = EventStatus.Active;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockMediator = new Mock<IMediator>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(new Mock<HttpContext>().Object);
            mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(x => x.GetCurrentUser(It.IsAny<ClaimsPrincipal>()))
                .Returns(new UserDto { Id = userId });

            service = new EventStatusHistoryService(
                mockMediator.Object,
                httpContextAccessor.Object,
                mockAuthService.Object,
                Context);
        }

        [Test]
        [Category("Set status event")]
        public void SetStatusEvent_InvalidEventId_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.SetStatusEvent(eventId, reason, eventStatus));
        }

        [Test]
        [Category("Set status event")]
        public void SetStatusEvent_CreateEventStatus_ReturnTrue()
        {
            var result = service.SetStatusEvent(eventId, reason, eventStatus);

            Assert.IsNotNull(result);
        }

        // [Test]
        // [Category("Set status event")]
        // public void SetStatusEvent_CreatedEventStatus_ReturnTrue()
        // {
        //    Assert.DoesNotThrowAsync(async () => await service.SetStatusEvent(eventId, reason, eventStatus));
        // }
        [Test]
        [Category("Create event status record")]
        public void CreateEventStatusRecord_EventCreated_ReturnTrue()
        {
            var ev = new Event()
            {
                Title = "testTitle",
            };
            var result = service.CreateEventStatusRecord(ev, reason, eventStatus);
            Assert.IsNotNull(result);
        }
    }
}
