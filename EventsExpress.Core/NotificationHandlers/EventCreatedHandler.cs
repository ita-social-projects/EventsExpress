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
    public class EventCreatedHandler : INotificationHandler<EventCreatedMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;

        public EventCreatedHandler(
            IEmailService sender,
            IUserService userSrv
            )
        {
            _sender = sender;
            _userService = userSrv;
        }

        public async Task Handle(EventCreatedMessage notification, CancellationToken cancellationToken)
        {
            var users = _userService.GetCategoriesFollowers(notification.Event.Categories);
            try
            {
                foreach (var u in users)
                {
                    await _sender.SendEmailAsync(new EmailDTO
                    {
                        SenderEmail = "noreply@eventService.com",
                        RecepientEmail = u.Email,
                        MessageText = $"The <a href='http://localhost:64144/event/{notification.Event.Id}/1'>event</a> was created which could interested you."
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
