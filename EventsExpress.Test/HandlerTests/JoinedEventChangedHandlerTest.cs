using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.NotificationHandlers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class JoinedEventChangedHandlerTest
    {
        private readonly Guid _idEvent = Guid.NewGuid();
        private Mock<IEmailService> _emailService;
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private JoinedEventChangedHandler _joinedEventChangedHandler;
        private JoinedEventMessage _joinedEventMessage;

        private Guid firstIdUser = Guid.NewGuid();
        private Guid secondIdUser = Guid.NewGuid();
        private Guid thirdIdUser = Guid.NewGuid();

        private string firstEmail = "first@gmail.com";
        private string secondEmail = "second@gmail.com";
        private string thirdEmail = "third@gmail.com";

        private UserDto firstUserDto;
        private UserDto secondUserDto;
        private UserDto thirdUserDto;

        private EventDto _eventDto;
        private UserEvent firstUser;
        private UserEvent secondUser;
        private UserEvent thirdUser;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();
            _appBaseUrl.Setup(it => it.Value.Host).Returns("https://localhost:44344");
            _joinedEventMessage = new JoinedEventMessage(_idEvent);
            _joinedEventChangedHandler = new JoinedEventChangedHandler(
                _emailService.Object,
                _userService.Object,
                _eventService.Object,
                _notificationTemplateService.Object,
                _appBaseUrl.Object);

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

            firstUser = new UserEvent()
            {
                UserId = firstIdUser,
            };
            secondUser = new UserEvent
            {
                UserId = secondIdUser,
            };
            thirdUser = new UserEvent
            {
                UserId = thirdIdUser,
            };
            _eventDto = new EventDto
            {
                Id = _idEvent,
                Visitors = new[]
                {
                    firstUser, secondUser, thirdUser,
                },
            };
            _eventService.Setup(e => e.EventById(It.IsAny<Guid>())).Returns(_eventDto);
            _notificationTemplateService
                .Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync(new NotificationTemplateDto { Id = It.IsAny<NotificationProfile>() });
            _notificationTemplateService.Setup(s =>
                    s.GetModelByTemplateId<EventChangeNotificationTemplateModel>(It.IsAny<NotificationProfile>()))
                .Returns(new EventChangeNotificationTemplateModel());
        }

        [Test]
        public void Handle_ThreeVisitorsHaveTurnedOnNotification_ThreeVisitorsReceiveNotification()
        {
            _userService.Setup(item => item.GetUsersByNotificationTypes(
                It.IsAny<NotificationChange>(),
                It.IsAny<IEnumerable<Guid>>())).Returns(new[] { firstUserDto, secondUserDto, thirdUserDto });

            var result = _joinedEventChangedHandler.Handle(_joinedEventMessage, CancellationToken.None);

            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
        }

        [Test]
        public void Handle_NoVisitorsHaveTurnedOnNotification_NobodyReceivesNotification()
        {
            _userService.Setup(item => item.GetUsersByNotificationTypes(
                It.IsAny<NotificationChange>(),
                It.IsAny<IEnumerable<Guid>>())).Returns(Enumerable.Empty<UserDto>());

            var result = _joinedEventChangedHandler.Handle(_joinedEventMessage, CancellationToken.None);

            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(0));
        }

        [Test]
        public void Handle_Catches_Exception()
        {
            // Arrange
            _emailService.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _joinedEventChangedHandler.Handle(_joinedEventMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
