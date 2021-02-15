namespace EventsExpress.Test.HandlerTests
{
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
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;

    internal class CreateEventVerificationHandlerTests : TestInitializer
    {
        private Mock<ILogger<CreateEventVerificationHandler>> _logger;
        private Mock<IEmailService> _emailService;
        private Mock<IUserService> _userService;
        private NotificationChange _nameNotification = NotificationChange.OwnEvent;
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
        protected override void Initialize()
        {
            base.Initialize();
            _logger = new Mock<ILogger<CreateEventVerificationHandler>>();
            _emailService = new Mock<IEmailService>();
            _userService = new Mock<IUserService>();
            _eventVerificationHandler = new CreateEventVerificationHandler(_logger.Object, _emailService.Object, _userService.Object, Context);
            _eventScheduleDto = new EventScheduleDto
            {
                Id = _idEventSchedule,
            };
            _createEventVerificationMessage = new CreateEventVerificationMessage(_eventScheduleDto);
            _changeInfo = new ChangeInfo
            {
                EntityName = "EventSchedule",
                EntityKeys = $"{_eventScheduleDto.Id}",
                ChangesType = ChangesType.Create,
                UserId = _idUser,
            };
            Context.ChangeInfos.Add(_changeInfo);
            Context.SaveChangesAsync();
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
            _userService.Setup(u => u.GetUsersByNotificationTypes(_nameNotification, _usersIds)).Returns(new UserDto[] { _userDto });
            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
            AppHttpContext.Configure(httpContext.Object);
        }

        [Test]
        public void Handle_AllUser_AllSubscribingUsers()
        {
            var result = _eventVerificationHandler.Handle(_createEventVerificationMessage, CancellationToken.None);
            _emailService.Verify(e => e.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(1));
        }
    }
}
