using System;
using System.Threading;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationHandlers;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    internal class BlockedEventHandlerTests : TestInitializer
    {
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private Mock<IEventService> _eventService;
        private NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private BlockedEventHandler _blockedEventHandler;
        private Guid[] usersId;
        private Guid firstIdUser = Guid.NewGuid();
        private Guid secondIdUser = Guid.NewGuid();
        private Guid thirdIdUser = Guid.NewGuid();
        private Guid fouthIdUser = Guid.NewGuid();
        private Guid fifthIdUser = Guid.NewGuid();
        private UserDto firstUserDto;
        private UserDto secondUserDto;
        private UserDto thirdUserDto;
        private string firstEmail = "first@gmail.com";
        private string secondEmail = "second@gmail.com";
        private string thirdEmail = "third@gmail.com";
        private BlockedEventMessage blockedEventMessage;
        private Guid idBlockedEventMessage = Guid.NewGuid();
        private Guid idEvent = Guid.NewGuid();
        private string eventTitle = "event title";
        private EventDto _eventDto;
        private EmailDto _emailDto;

        [SetUp]
        protected override void Initialize()
        {
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _eventService = new Mock<IEventService>();
            _blockedEventHandler = new BlockedEventHandler(_emailService.Object, _userService.Object, _eventService.Object);
            firstUserDto = new UserDto
            {
                Id = firstIdUser,
                Email = firstEmail,
            };
            secondUserDto = new UserDto
            {
                Id = secondIdUser,
                Email = secondEmail,
            };
            thirdUserDto = new UserDto
            {
                Id = thirdIdUser,
                Email = thirdEmail,
            };
            usersId = new Guid[] { firstIdUser, secondIdUser, thirdIdUser, fouthIdUser, fifthIdUser };
            blockedEventMessage = new BlockedEventMessage(usersId, idBlockedEventMessage);
            _eventDto = new EventDto
            {
                Id = idBlockedEventMessage,
                Title = eventTitle,
            };
            _emailDto = new EmailDto { MessageText = "text", RecepientEmail = "email", Subject = "subject" };

            _userService.Setup(item => item.GetUsersByNotificationTypes(_nameNotification, usersId)).Returns(new UserDto[] { firstUserDto, secondUserDto, thirdUserDto });

            _eventService.Setup(item => item.EventById(_eventDto.Id)).Returns(_eventDto);
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
            AppHttpContext.Configure(httpContext.Object);
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _blockedEventHandler.Handle(blockedEventMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(3));
            _eventService.Verify(e => e.EventById(It.IsAny<Guid>()), Times.Exactly(3));
        }
    }
}
