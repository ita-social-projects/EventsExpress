using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsExpress.Core.NotificationHandlers
{
    public class BlockedEventHandler : INotificationHandler<BlockedEventMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public BlockedEventHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService
            )
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
        }

        public async Task Handle(BlockedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var Email = _userService.GetById(notification.UserId).Email;
                var Even = _eventService.EventById(notification.Id);
                var EventLink = "https://eventsexpress.azurewebsites.net/event/" + notification.Id + "/1";
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Your event was blocked",
                    RecepientEmail = Email,
                    MessageText = $"Dear {Email}, your event was blocked for some reason. " +
                    $"To unblock it, edit this event, please: " +
                    $"\"<a href='{EventLink}'>{Even.Title}</>\""
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
