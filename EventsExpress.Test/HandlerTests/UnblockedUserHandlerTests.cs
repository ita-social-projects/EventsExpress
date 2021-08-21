using System;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.NotificationTemplateModels;
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
    internal class UnblockedUserHandlerTests
    {
        private Mock<IHubContext<UsersHub, IUsersClient>> _usersHubContext;
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<ICacheHelper> _cacheHelper;
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
            _cacheHelper = new Mock<ICacheHelper>();
            _usersHubContext = new Mock<IHubContext<UsersHub, IUsersClient>>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();

            _notificationTemplateService.Setup(s =>
                    s.GetModelByTemplateId<AccountStatusNotificationTemplateModel>(It.IsAny<NotificationProfile>()))
                .Returns(new AccountStatusNotificationTemplateModel());
            _notificationTemplateService
                .Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync(new NotificationTemplateDto { Id = It.IsAny<NotificationProfile>() });

            _unBlockedUserHandler = new UnblockedUserHandler(
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
            _unBlockedUserMessage = new UnblockedAccountMessage(_account);
            _usersIds = new Guid[] { _idUser };
            _userService.Setup(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds)).Returns(new UserDto[] { _userDto });
        }

        [Test]
        public async Task Handle_AllUser_AllSubscribingUsers()
        {
            await _unBlockedUserHandler.Handle(_unBlockedUserMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }

        [Test]
        public async Task UsersHub_Sends_ToAllUsers()
        {
            // Arrange
            _usersHubContext.Setup(s => s.Clients.All.CountUsers())
                .Returns(Task.CompletedTask);

            // Act
            await _unBlockedUserHandler.Handle(_unBlockedUserMessage, CancellationToken.None);

            // Assert
            _usersHubContext.Verify(s => s.Clients.All.CountUsers(), Times.Once);
        }

        [Test]
        public void Handle_Catches_exception()
        {
            // Arrange
            _emailService.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _unBlockedUserHandler.Handle(_unBlockedUserMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
