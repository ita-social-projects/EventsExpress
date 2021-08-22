using System;
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
    [TestFixture]
    internal class RegisterVerificationHandlerTests
    {
        private Mock<IEmailService> _sender;
        private Mock<ILogger<RegisterVerificationHandler>> _logger;
        private Mock<IOptions<AppBaseUrlModel>> _appBaseUrl;
        private Mock<INotificationTemplateService> _notificationTemplateService;
        private Mock<ITokenService> _tockenServiece;
        private RegisterVerificationHandler _registerVerificationHandler;
        private RegisterVerificationMessage _message;

        [SetUp]
        public void Initialize()
        {
            _sender = new Mock<IEmailService>();
            _logger = new Mock<ILogger<RegisterVerificationHandler>>();
            _notificationTemplateService = new Mock<INotificationTemplateService>();
            _appBaseUrl = new Mock<IOptions<AppBaseUrlModel>>();
            _tockenServiece = new Mock<ITokenService>();

            _appBaseUrl.Setup(x => x.Value.Host).Returns("https://localhost:44344");

            _notificationTemplateService.Setup(s =>
                    s.GetModelByTemplateId<RegisterVerificationNotificationTemplateModel>(It.IsAny<NotificationProfile>()))
                .Returns(new RegisterVerificationNotificationTemplateModel());
            _notificationTemplateService.Setup(
                service => service.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync((NotificationProfile id) => new NotificationTemplateDto
                {
                    Id = id,
                    Title = "testTitle",
                    Subject = "testSubject",
                    Message = "testMessage",
                });

            _message = new RegisterVerificationMessage(new AuthLocal
            {
                Id = default,
                Email = "testemail@gmail.com",
                Account = new Account(),
                AccountId = default,
                EmailConfirmed = true,
                PasswordHash = "testHash",
                Salt = "testSalt",
            });

            _registerVerificationHandler = new RegisterVerificationHandler(
                _sender.Object,
                _logger.Object,
                _notificationTemplateService.Object,
                _appBaseUrl.Object,
                _tockenServiece.Object);

            var httpContext = new Mock<IHttpContextAccessor>();
            httpContext.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());
        }

        [Test]
        public async Task NotificationTemplateService_GetByIdAsync_IsInvoked()
        {
            // Act
            await _registerVerificationHandler.Handle(_message, CancellationToken.None);

            // Assert
            _notificationTemplateService.Verify(
                service => service.GetByIdAsync(It.IsAny<NotificationProfile>()),
                Times.Once);
        }

        [Test]
        public void Handle_Catches_exception()
        {
            // Arrange
            _sender.Setup(s => s.SendEmailAsync(It.IsAny<EmailDto>()))
                .ThrowsAsync(new Exception("Some reason!"));

            // Act
            var actual = _registerVerificationHandler.Handle(_message, CancellationToken.None);

            // Assert
            Assert.AreEqual(Task.CompletedTask.Status, actual.Status);
        }
    }
}
