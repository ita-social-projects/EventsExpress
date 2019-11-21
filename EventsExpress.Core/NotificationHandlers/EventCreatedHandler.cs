using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<HostSettings> _urlOptions;

        public EventCreatedHandler(
            IEmailService sender,
            IUserService userSrv,
            IOptions<HostSettings> opt
            )
        {
            _sender = sender;
            _userService = userSrv;
            _urlOptions = opt;
        }

        public async Task Handle(EventCreatedMessage notification, CancellationToken cancellationToken)
        {
            var users = _userService.GetUsersByCategories(notification.Event.Categories);
            try
            {
                foreach (var u in users)
                {
                    var host = _urlOptions.Value.Host;
                    var port = _urlOptions.Value.Port;

                    string link = $"{host}:{port}/event/{notification.Event.Id}/1";

                    await _sender.SendEmailAsync(new EmailDTO
                    {
                        Subject = "New event for you!",
                        RecepientEmail = u.Email,
                        MessageText = $"The <a href='{link}'>event</a> was created which could interested you."
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
