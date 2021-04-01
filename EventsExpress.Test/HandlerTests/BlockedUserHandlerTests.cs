using System;
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
    internal class BlockedUserHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private BlockedUserHandler _blockedUserHandler;
        private Guid _idUser = Guid.NewGuid();
        private string _emailUser = "user@gmail.com";
        private User _user;
        private UserDto _userDto;
        private BlockedUserMessage _blockedUserMessage;
        private NotificationChange _notificationChange = NotificationChange.Profile;
        private Guid[] _usersIds;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _blockedUserHandler = new BlockedUserHandler(_emailService.Object, _userService.Object, _notificationTemplateService.Object);
            _user = new User
            {
                Id = _idUser,
                Email = _emailUser,
            };
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _emailUser,
            };
            _blockedUserMessage = new BlockedUserMessage(_user);
            _usersIds = new Guid[] { _idUser };
            _userService.Setup(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds)).Returns(new UserDto[] { _userDto });
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _blockedUserHandler.Handle(_blockedUserMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }
    }
}
