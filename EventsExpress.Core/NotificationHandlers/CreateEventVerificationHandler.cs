using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventsExpress.Core.NotificationHandlers
{
    public class CreateEventVerificationHandler : INotificationHandler<CreateEventVerificationMessage>
    {
        private readonly ILogger<CreateEventVerificationHandler> _logger;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly ITrackService _trackService;
        private readonly INotificationTemplateService _messageService;

        public CreateEventVerificationHandler(
            ILogger<CreateEventVerificationHandler> logger,
            IEmailService sender,
            IUserService userService,
            ITrackService trackService,
            INotificationTemplateService messageService)
        {
            _logger = logger;
            _sender = sender;
            _userService = userService;
            _trackService = trackService;
            _messageService = messageService;
        }

        public async Task Handle(CreateEventVerificationMessage notification, CancellationToken cancellationToken)
        {
            var changeInfos = await _trackService.GetChangeInfoByScheduleIdAsync(notification.EventSchedule.Id);

            if (changeInfos == null)
            {
                return;
            }

            try
            {
                var usersId = new[] { changeInfos.UserId };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, usersId).Select(x => x.Email).SingleOrDefault();

                var message = await _messageService.GetByIdAsync(NotificationProfile.CreateEventVerification);

                if (userEmail != null)
                {
                    string link = $"{AppHttpContext.AppBaseUrl}/eventSchedule/{notification.EventSchedule.Id}";

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                        { "(link)", link },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _messageService.PerformReplacement(message.Subject, pattern),
                        RecepientEmail = userEmail,
                        MessageText = _messageService.PerformReplacement(message.MessageText, pattern),
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
