using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Hubs;
using EventsExpress.Hubs.Clients;
using EventsExpress.NotificationHandlers;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class BlockedUserHandlerTests
    {
        private readonly NotificationChange _notificationChange = NotificationChange.Profile;
        private readonly string _emailUser = "user@gmail.com";
        private Mock<IHubContext<UsersHub, IUsersClient>> _usersHubContext;
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
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _usersHubContext = new Mock<IHubContext<UsersHub, IUsersClient>>();

            _notificationTemplateService
                .Setup(service => service.GetByIdAsync(NotificationProfile.BlockedUser))
                .ReturnsAsync(new NotificationTemplateDto { Id = NotificationProfile.BlockedUser });

            _blockedUserHandler = new BlockedUserHandler(
                _emailService.Object,
                _userService.Object,
                _notificationTemplateService.Object,
                _usersHubContext.Object);

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

        [Test]
        public async Task UsersHub_Sends_ToAllUsers()
        {
            // Arrange
            _usersHubContext.Setup(s => s.Clients.All.CountUsers())
                .Returns(Task.CompletedTask);

            // Act
            await _blockedUserHandler.Handle(_blockedUserMessage, CancellationToken.None);

            // Assert
            _usersHubContext.Verify(s => s.Clients.All.CountUsers(), Times.Once);
        }
    }
}
