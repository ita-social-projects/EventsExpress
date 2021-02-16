using System;
using System.Collections.Generic;
using System.Threading;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationHandlers;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class CancelEventHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IEventService> _eventService;
        private Mock<IEventStatusHistoryService> _eventStatusHistoryService;
        private Mock<IUserService> _userService;
        private CancelEventHandler _cancelEventHandler;
        private Guid _idEvent = Guid.NewGuid();
        private Guid _idUser = Guid.NewGuid();
        private Guid[] usersId;
        private Guid firstIdUser = Guid.NewGuid();
        private Guid secondIdUser = Guid.NewGuid();
        private Guid thirdIdUser = Guid.NewGuid();
        private Guid fouthIdUser = Guid.NewGuid();
        private Guid fifthIdUser = Guid.NewGuid();
        private UserDto firstUserDto;
        private UserDto secondUserDto;
        private UserDto thirdUserDto;
        private UserEvent firstUserEvent;
        private UserEvent secondUserEvent;
        private UserEvent thirdUserEvent;
        private string firstEmail = "first@gmail.com";
        private string secondEmail = "second@gmail.com";
        private string thirdEmail = "third@gmail.com";
        private CancelEventMessage _cancelEventMessage;
        private Event _event;
        private EventDto _eventDto;
        private EventStatusHistory _eventStatusHistory;
        private string _reason = "some reason";

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _eventService = new Mock<IEventService>();
            _eventStatusHistoryService = new Mock<IEventStatusHistoryService>();
            _userService = new Mock<IUserService>();
            _event = new Event
            {
                Id = _idEvent,
            };
            _cancelEventMessage = new CancelEventMessage(_idEvent);
            firstUserDto = new UserDto
            {
                Id = firstIdUser,
                Email = firstEmail,
            };
            secondUserDto = new UserDto
            {
                Id = secondIdUser,
                Email = secondEmail,
            };
            thirdUserDto = new UserDto
            {
                Id = thirdIdUser,
                Email = thirdEmail,
            };
            firstUserEvent = new UserEvent
            {
                UserId = firstIdUser,
            };
            secondUserEvent = new UserEvent
            {
                UserId = secondIdUser,
            };
            thirdUserEvent = new UserEvent
            {
                UserId = thirdIdUser,
            };
            usersId = new Guid[] { firstIdUser, secondIdUser, thirdIdUser, fouthIdUser, fifthIdUser };
            _eventDto = new EventDto
            {
                Id = _idEvent,
                Visitors = new UserEvent[]
                {
                   firstUserEvent, secondUserEvent, thirdUserEvent,
                },
            };

            _eventService.Setup(e => e.EventById(_idEvent)).Returns(_eventDto);
            _userService.Setup(item => item.GetUsersByNotificationTypes(It.IsAny<NotificationChange>(), It.IsAny<IEnumerable<Guid>>())).Returns(new UserDto[] { firstUserDto, secondUserDto, thirdUserDto });
            _eventStatusHistory = new EventStatusHistory
            {
                Reason = _reason,
                EventStatus = EventStatus.Cancelled,
            };
            _eventStatusHistoryService.Setup(e => e.GetLastRecord(It.IsAny<Guid>(), EventStatus.Cancelled)).Returns(_eventStatusHistory);
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
            AppHttpContext.Configure(httpContext.Object);
            _cancelEventHandler = new CancelEventHandler(_emailService.Object, _eventService.Object, _eventStatusHistoryService.Object, _userService.Object);
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _cancelEventHandler.Handle(_cancelEventMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
        }
    }
}
