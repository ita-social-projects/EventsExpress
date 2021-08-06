using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.NotificationHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class EventStatusHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IEventService> _eventService;
        private Mock<IEventStatusHistoryService> _cancelEventStatusHistoryService;
        private Mock<IEventStatusHistoryService> _blockEventStatusHistoryService;
        private Mock<IEventStatusHistoryService> _unblockEventStatusHistoryService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private EventStatusHandler _cancelEventHandler;
        private EventStatusHandler _blockEventHandler;
        private EventStatusHandler _unblockEventHandler;
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
        private EventStatusMessage _cancelEventStatusMessage;
        private EventStatusMessage _blockEventStatusMessage;
        private EventStatusMessage _unblockEventStatusMessage;
        private Event _event;
        private EventDto _eventDto;
        private EventStatusHistory _eventStatusHistoryCanceled;
        private EventStatusHistory _eventStatusHistoryBlocked;
        private EventStatusHistory _eventStatusHistoryUnBlocked;
        private string _reason = "some reason";

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _cancelEventStatusHistoryService = new Mock<IEventStatusHistoryService>();
            _blockEventStatusHistoryService = new Mock<IEventStatusHistoryService>();
            _unblockEventStatusHistoryService = new Mock<IEventStatusHistoryService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();

            _appBaseUrl.Setup(x => x.Value.Host).Returns("https://localhost:44344");

            _event = new Event
            {
                Id = _idEvent,
            };
            _cancelEventStatusMessage = new EventStatusMessage(_idEvent, "testReason", EventStatus.Canceled);
            _blockEventStatusMessage = new EventStatusMessage(_idEvent, "testReason", EventStatus.Blocked);
            _unblockEventStatusMessage = new EventStatusMessage(_idEvent, "testReason", EventStatus.Active);
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

            _notificationTemplateService
                .Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync(new NotificationTemplateDto { Id = It.IsAny<NotificationProfile>() });

            _eventStatusHistoryCanceled = new EventStatusHistory
            {
                Reason = _reason,
                EventStatus = EventStatus.Canceled,
            };
            _eventStatusHistoryBlocked = new EventStatusHistory
            {
                Reason = _reason,
                EventStatus = EventStatus.Blocked,
            };
            _eventStatusHistoryUnBlocked = new EventStatusHistory
            {
                Reason = _reason,
                EventStatus = EventStatus.Active,
            };
            _cancelEventStatusHistoryService.Setup(e => e.GetLastRecord(It.IsAny<Guid>(), EventStatus.Canceled)).Returns(_eventStatusHistoryCanceled);
            _blockEventStatusHistoryService.Setup(e => e.GetLastRecord(It.IsAny<Guid>(), EventStatus.Blocked)).Returns(_eventStatusHistoryBlocked);
            _unblockEventStatusHistoryService.Setup(e => e.GetLastRecord(It.IsAny<Guid>(), EventStatus.Active)).Returns(_eventStatusHistoryUnBlocked);
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
            _cancelEventHandler = new EventStatusHandler(_emailService.Object, _userService.Object, _eventService.Object, _notificationTemplateService.Object, _appBaseUrl.Object);
            _blockEventHandler = new EventStatusHandler(_emailService.Object, _userService.Object, _eventService.Object, _notificationTemplateService.Object, _appBaseUrl.Object);
            _unblockEventHandler = new EventStatusHandler(_emailService.Object, _userService.Object, _eventService.Object, _notificationTemplateService.Object, _appBaseUrl.Object);
        }

        [Test]
        public void Handle_AllCanceledUsers_AllSubscribingUsers()
        {
            var result = _cancelEventHandler.Handle(_cancelEventStatusMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
        }

        [Test]
        public void Handle_AllBlockedUsers_AllSubscribingUsers()
        {
            var result = _blockEventHandler.Handle(_blockEventStatusMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
        }

        [Test]
        public void Handle_AllUnBlockUsers_AllSubscribingUsers()
        {
            var result = _unblockEventHandler.Handle(_unblockEventStatusMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
        }

        [Test]
        public void Handle_Catches_exception()
        {
            // Arrange
            _emailService.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _unblockEventHandler.Handle(_unblockEventStatusMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
