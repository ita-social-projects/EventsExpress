using System;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventsExpress.Core.NotificationHandlers
{
    public class BlockedEventHandler : INotificationHandler<BlockedEventMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly ILogger _logger;

        public BlockedEventHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService,
            ILogger logger)
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
            _logger = logger;
        }

        public async Task Handle(BlockedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var email = _userService.GetById(notification.UserId).Email;
                var even = _eventService.EventById(notification.Id);
                string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";

                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Your event was blocked",
                    RecepientEmail = email,
                    MessageText = $@"Dear {email}.
Your event was blocked for some reason. 
To unblock it, edit this event, please: <a href='{eventLink}'>{even.Title}</>",
                });
            }
            catch (Ex
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
