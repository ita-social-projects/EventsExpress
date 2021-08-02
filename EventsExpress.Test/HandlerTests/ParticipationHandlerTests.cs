﻿using System;
using System.Collections.Generic;
using System.Threading;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
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
    internal class ParticipationHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private ParticipationHandler _participationHandler;
        private Guid _idUser = Guid.NewGuid();
        private string _emailUser = "user@gmail.com";
        private User _user;
        private UserDto _userDto;
        private ParticipationMessage _participationMessage;
        private Guid _idParticipationMessage = Guid.NewGuid();
        private UserStatusEvent _participationStatus = UserStatusEvent.Approved;
        private NotificationChange _notificationChange = NotificationChange.VisitedEvent;
        private Guid[] _usersIds;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();

            _notificationTemplateService
                .Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync(new NotificationTemplateDto { Id = It.IsAny<NotificationProfile>() });

            _notificationTemplateService
                .Setup(s => s.PerformReplacement(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(string.Empty);

            _appBaseUrl.Setup(x => x.Value.Host).Returns("https://localhost:44344");

            _participationHandler = new ParticipationHandler(_emailService.Object, _userService.Object, _notificationTemplateService.Object, _appBaseUrl.Object);
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

            _participationMessage = new ParticipationMessage(_idUser, _idParticipationMessage, _participationStatus);
            _usersIds = new Guid[] { _idUser };
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            _userService.Setup(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds)).Returns(new UserDto[] { _userDto });
            var result = _participationHandler.Handle(_participationMessage, CancellationToken.None);
            _userService.Verify(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds), Times.Exactly(1));
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }

        [Test]
        public void Handle_AllUser_Exception()
        {
            _userService.Setup(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds)).Throws<Exception>();

            Assert.DoesNotThrowAsync(() => _participationHandler.Handle(_participationMessage, CancellationToken.None));
            _userService.Verify(u => u.GetUsersByNotificationTypes(_notificationChange, _usersIds), Times.Exactly(1));
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(0));
        }
    }
}
