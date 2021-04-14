using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventsExpress.Core.NotificationHandlers
{
    public class RegisterVerificationHandler : INotificationHandler<RegisterVerificationMessage>
    {
        private readonly IEmailService _sender;
        private readonly ICacheHelper _cacheHepler;
        private readonly ILogger<RegisterVerificationHandler> _logger;
        private readonly INotificationTemplateService _messageService;

        public RegisterVerificationHandler(
            IEmailService sender,
            ICacheHelper cacheHepler,
            ILogger<RegisterVerificationHandler> logger,
            INotificationTemplateService messageService)
        {
            _sender = sender;
            _cacheHepler = cacheHepler;
            _logger = logger;
            _messageService = messageService;
        }

        public async Task Handle(RegisterVerificationMessage notification, CancellationToken cancellationToken)
        {
            var token = Guid.NewGuid().ToString();
            string theEmailLink = $"<a \" target=\"_blank\" href=\"{AppHttpContext.AppBaseUrl}/authentication/{notification.User.Id}/{token}\">link</a>";

            _cacheHepler.Add(new CacheDto
            {
                UserId = notification.User.Id,
                Token = token,
            });

            var message = await _messageService.GetByIdAsync(NotificationProfile.RegisterVerification);

            Dictionary<string, string> pattern = new Dictionary<string, string>
            {
                { "(link)", theEmailLink },
            };

            try
            {
                await _sender.SendEmailAsync(new EmailDto
                {
                    Subject = _messageService.PerformReplacement(message.Subject, pattern),
                    RecepientEmail = notification.User.Email,
                    MessageText = _messageService.PerformReplacement(message.MessageText, pattern),
                });

                _cacheHepler.GetValue(notification.User.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
