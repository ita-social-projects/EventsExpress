using System;
using System.Collections.Generic;
using System.Threading;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationHandlers;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class UnBlockedUserHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private UnblockedUserHandler _unBlockedUserHandler;
        private Guid _idUser = Guid.NewGuid();
        private string _emailUser = "user@gmail.com";
        private Account _account;
        private UserDto _userDto;
        private UnblockedAccountMessage _unBlockedUserMessage;
        private NotificationChange _notificationChange = NotificationChange.Profile;
        private Guid[] _usersIds;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();

            _notificationTemplateService
                .Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync(new NotificationTemplateDTO { Id = It.IsAny<NotificationProfile>() });

            _notificationTemplateService
                .Setup(s => s.PerformReplacement(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(string.Empty);
            _unBlockedUserHandler = new UnblockedUserHandler(_emailService.Object, _userService.Object, _notificationTemplateService.Object);
            _account = new Account
            {
                UserId = _idUser,
            };
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _emailUser,
            };
            _unBlockedUserMessage = new UnblockedAccountMessage(_account);
            _usersIds = new Guid[] { _idUser };
            _userService.Setup(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds)).Returns(new UserDto[] { _userDto });
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _unBlockedUserHandler.Handle(_unBlockedUserMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }
    }
}
