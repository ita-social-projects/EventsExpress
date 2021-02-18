using System;
using System.Threading;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationHandlers;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class EventCreatedHandlerTests
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
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
            _eventCreatedHandler = new EventCreatedHandler(_emailService.Object, _userService.Object);
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
            AppHttpContext.Configure(httpContext.Object);
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _eventCreatedHandler.Handle(_eventCreatedMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }
    }
}
