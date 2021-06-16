using System;
using System.Collections.Generic;
using System.Threading;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Hubs;
using EventsExpress.NotificationHandlers;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class BlockedUserHandlerTests
    {
        private readonly NotificationChange _notificationChange = NotificationChange.Profile;
        private readonly string _emailUser = "user@gmail.com";
        private UsersHub _usersHub;
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<ICacheHelper> _cacheHelper;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private BlockedUserHandler _blockedUserHandler;
        private Guid _idUser = Guid.NewGuid();
        private UserDto _userDto;
        private BlockedAccountMessage _blockedUserMessage;
        private Guid[] _usersIds;
        private Account _account;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _cacheHelper = new Mock<ICacheHelper>();
            _usersHub = new UsersHub(
                _cacheHelper.Object,
                _userService.Object);
            _notificationTemplateService = new Mock<INotificationTemplateService>();

            _notificationTemplateService
                .Setup(service => service.GetByIdAsync(NotificationProfile.BlockedUser))
                .ReturnsAsync(new NotificationTemplateDto { Id = NotificationProfile.BlockedUser });

            _notificationTemplateService
                .Setup(s => s.PerformReplacement(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(string.Empty);

            _blockedUserHandler = new BlockedUserHandler(
                _usersHub,
                _emailService.Object,
                _userService.Object,
                _notificationTemplateService.Object);

            _account = new Account
            {
                UserId = _idUser,
            };
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _emailUser,
            };
            _blockedUserMessage = new BlockedAccountMessage(_account);
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
