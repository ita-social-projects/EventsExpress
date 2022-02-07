using System;
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
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class OwnEventChangedHandlerTest
    {
        private readonly Guid _idEvent = Guid.NewGuid();
        private Mock<IEmailService> _emailService;
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private OwnEventChangedHandler _ownEventChangedHandler;
        private OwnEventMessage _ownEventMessage;

        [SetUp]
        public void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();
            _appBaseUrl.Setup(it => it.Value.Host).Returns("https://localhost:44344");
            _ownEventMessage = new OwnEventMessage(_idEvent);
            _ownEventChangedHandler = new OwnEventChangedHandler(
                _emailService.Object,
                _userService.Object,
                _eventService.Object,
                _notificationTemplateService.Object,
                _appBaseUrl.Object);
        }

        [Test]
        public void Handle_AllCanceledUsers_AllSubscribingUsers()
        {
            var result = _ownEventChangedHandler.Handle(_ownEventMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
        }
    }
}
