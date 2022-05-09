using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.NotificationHandlers;
using Moq;
using NUnit.Framework;
using Role = EventsExpress.Db.Enums.Role;

namespace EventsExpress.Test.HandlerTests
{
    internal class RoleChangedHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private RoleChangeHandler _roleChangedHandler;
        private Guid userID = Guid.NewGuid();
        private string userEmail = "first@gmail.com";
        private UserDto userDto;
        private RoleChangeMessage _roleChangedMessage;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();

            _notificationTemplateService.Setup(s =>
                    s.GetModelByTemplateId<RoleChangeNotificationTemplateModel>(It.IsAny<NotificationProfile>()))
                .Returns(new RoleChangeNotificationTemplateModel());
            _notificationTemplateService
                 .Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                 .ReturnsAsync(new NotificationTemplateDto { Id = It.IsAny<NotificationProfile>() });

            var userIds = new[] { userID };
            userDto = new UserDto
            {
                Id = userID,
                Email = userEmail,
            };
            _userService.Setup(it => it.GetUsersByNotificationTypes(NotificationChange.Profile, userIds))
                .Returns(new[] { userDto });

            _roleChangedHandler = new RoleChangeHandler(
                _notificationTemplateService.Object,
                _userService.Object,
                _emailService.Object);

            var account = new Account
            {
                UserId = userID,
            };
            var roles = new[]
            {
                new AccountRole
                {
                    RoleId = Role.Admin,
                },
                new AccountRole
                {
                    RoleId = Role.User,
                },
            };
            _roleChangedMessage = new RoleChangeMessage(account, roles);
        }

        [Test]
        public async Task Handle_SendEmailToUser_Success()
        {
            await _roleChangedHandler.Handle(_roleChangedMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }

        [Test]
        public void Handle_Catches_exception()
        {
            // Arrange
            _emailService.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _roleChangedHandler.Handle(_roleChangedMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }

        [Test]
        public void Handle_GetUserByNotificationType_ReturnsException()
        {
            // Arrange
            _userService.Setup(s => s.GetUsersByNotificationTypes(
                    NotificationChange.Profile,
                    It.IsAny<IEnumerable<Guid>>()))
                .Throws(new Exception());

            // Act
            var actual = _roleChangedHandler.Handle(_roleChangedMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
