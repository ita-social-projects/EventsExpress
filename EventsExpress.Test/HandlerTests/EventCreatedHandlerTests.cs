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
    internal class EventCreatedHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private EventCreatedHandler _eventCreatedHandler;
        private Guid _idUser = Guid.NewGuid();
        private string _emailUser = "user@gmail.com";
        private User _user;
        private UserDto _userDto;
        private EventCreatedMessage _eventCreatedMessage;
        private NotificationChange _notificationChange = NotificationChange.OwnEvent;
        private Guid[] _usersIds;
        private EventDto _eventDto;
        private Guid _idEventDto = Guid.NewGuid();

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            var templateId = NotificationProfile.EventCreated;
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();

            _appBaseUrl.Setup(x => x.Value.Host).Returns("https://localhost:44344");

            _notificationTemplateService
                .Setup(s => s.GetByIdAsync(templateId))
                .ReturnsAsync(new NotificationTemplateDto { Id = templateId });

            _notificationTemplateService
                .Setup(s => s.PerformReplacement(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(string.Empty);

            _eventCreatedHandler = new EventCreatedHandler(_emailService.Object, _userService.Object, _notificationTemplateService.Object, _appBaseUrl.Object);
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
            _eventDto = new EventDto
            {
                Id = _idEventDto,
            };
            _eventCreatedMessage = new EventCreatedMessage(_eventDto);
            _usersIds = new Guid[] { _idUser };
            _userService.Setup(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds)).Returns(new UserDto[] { _userDto });
            _userService.Setup(u => u.GetUsersByCategories(It.IsAny<CategoryDto[]>())).Returns(new UserDto[] { _userDto });
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _eventCreatedHandler.Handle(_eventCreatedMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }

        [Test]
        public void Handle_Catches_exception()
        {
            // Arrange
            _emailService.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _eventCreatedHandler.Handle(_eventCreatedMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
