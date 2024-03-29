﻿using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class CreateEventVerificationHandlerTests
    {
        private Mock<ILogger<CreateEventVerificationHandler>> _logger;
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<ITrackService> _trackService;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private CreateEventVerificationHandler _eventVerificationHandler;
        private CreateEventVerificationMessage _createEventVerificationMessage;
        private EventScheduleDto _eventScheduleDto;
        private Guid _idEventSchedule = Guid.NewGuid();
        private ChangeInfo _changeInfo;
        private Guid _idUser = Guid.NewGuid();
        private string _emailUser = "user@gmail.com";
        private User _user;
        private UserDto _userDto;
        private Guid[] _usersIds;

        [SetUp]
        public void Initialize()
        {
            _logger = new Mock<ILogger<CreateEventVerificationHandler>>();
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _trackService = new Mock<ITrackService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();

            _appBaseUrl.Setup(x => x.Value.Host).Returns("https://localhost:44344");

            _notificationTemplateService.Setup(s =>
                    s.GetModelByTemplateId<CreateEventVerificationNotificationTemplateModel>(It.IsAny<NotificationProfile>()))
                .Returns(new CreateEventVerificationNotificationTemplateModel());
            _notificationTemplateService.Setup(s =>
                    s.GetByIdAsync(It.IsAny<NotificationProfile>()))
               .ReturnsAsync(new NotificationTemplateDto { Id = It.IsAny<NotificationProfile>() });

            _eventVerificationHandler = new CreateEventVerificationHandler(_logger.Object, _emailService.Object, _userService.Object, _trackService.Object, _notificationTemplateService.Object, _appBaseUrl.Object);
            _eventScheduleDto = new EventScheduleDto
            {
                Id = _idEventSchedule,
            };
            _createEventVerificationMessage = new CreateEventVerificationMessage(_eventScheduleDto);
            _changeInfo = new ChangeInfo
            {
                EntityName = "EventSchedule",
                EntityKeys = $"{{ \"Id\":\"{_eventScheduleDto.Id}\" }}",
                ChangesType = ChangesType.Create,
                UserId = _idUser,
            };
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
            _usersIds = new Guid[] { _idUser };
            _userService.Setup(u => u.GetUsersByNotificationTypes(It.IsAny<NotificationChange>(), It.IsAny<IEnumerable<Guid>>())).Returns(new UserDto[] { _userDto });
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            // Arrange
            _trackService.Setup(track => track.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_changeInfo));

            // Act
            var result = _eventVerificationHandler.Handle(_createEventVerificationMessage, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<Task>(result);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
            _trackService.Verify(track => track.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>()), Times.Exactly(1));
            _userService.Verify(u => u.GetUsersByNotificationTypes(It.IsAny<NotificationChange>(), It.IsAny<IEnumerable<Guid>>()), Times.Exactly(1));
        }

        [Test]
        public void Handle_AllUser_Nothing()
        {
            _trackService.Setup(track => track.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>())).Returns((Task<ChangeInfo> info) => null);

            var result = _eventVerificationHandler.Handle(_createEventVerificationMessage, CancellationToken.None);

            Assert.IsInstanceOf<Task>(result);
            _trackService.Verify(track => track.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>()), Times.Exactly(1));
            _userService.Verify(u => u.GetUsersByNotificationTypes(It.IsAny<NotificationChange>(), It.IsAny<IEnumerable<Guid>>()), Times.Exactly(0));
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(0));
        }

        [Test]
        public async Task Handle_Ends_work_if_changeInfo_is_null()
        {
            // Arrange
            _trackService.Setup(s => s.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ChangeInfo)null);

            // Act
            await _eventVerificationHandler.Handle(_createEventVerificationMessage, CancellationToken.None);

            // Assert
            _emailService.Verify(s => s.SendEmailAsync(It.IsAny<EmailDto>()), Times.Never);
        }

        [Test]
        public async Task Handle_continue_work_if_changeInfo_is_not_null()
        {
            // Arrange
            _trackService.Setup(s => s.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_changeInfo);

            // Act
            await _eventVerificationHandler.Handle(_createEventVerificationMessage, CancellationToken.None);

            // Assert
            _emailService.Verify(s => s.SendEmailAsync(It.IsAny<EmailDto>()), Times.Once);
        }

        [Test]
        public void Handle_Catches_exception()
        {
            // Arrange
            _trackService.Setup(s => s.GetChangeInfoByScheduleIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_changeInfo);
            _emailService.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _eventVerificationHandler.Handle(_createEventVerificationMessage, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
