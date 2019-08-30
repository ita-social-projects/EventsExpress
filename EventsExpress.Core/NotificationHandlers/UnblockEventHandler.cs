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
    public class UnblockedEventHandler : INotificationHandler<UnblockedEventMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public UnblockedEventHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService
            )
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
        }

        public async Task Handle(UnblockedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var Email = _userService.GetById(notification.UserId).Email;
                var Even = _eventService.EventById(notification.Id);
                var EventLink = "http://localhost:51712/event/" + notification.Id + "/1";
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Your event was UNblocked",
                    RecepientEmail = Email,
                    MessageText = $"Dear {Email}, congratulations, your event was UNblocked! " +
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
