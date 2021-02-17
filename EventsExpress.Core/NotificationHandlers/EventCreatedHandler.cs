using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.Core.NotificationHandlers
{
    public class EventCreatedHandler : INotificationHandler<EventCreatedMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;

        public EventCreatedHandler(
            IEmailService sender,
            IUserService userSrv)
        {
            _sender = sender;
            _userService = userSrv;
        }

        public async Task Handle(EventCreatedMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = _userService.GetUsersByCategories(notification.Event.Categories).Select(x => x.Id);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email);
                foreach (var userEmail in usersEmails)
                    {
                        string link = $"{AppHttpContext.AppBaseUrl}/event/{notification.Event.Id}/1";
                        await _sender.SendEmailAsync(new EmailDto
                        {
                            Subject = "New event for you!",
                            RecepientEmail = userEmail,
                            MessageText = $"The <a href='{link}'>event</a> was created which could interested you.",
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
