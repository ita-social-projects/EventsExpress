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
    public class BlockedEventHandler : INotificationHandler<BlockedEventMessage>
    {
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public BlockedEventHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService)
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
        }

        public async Task Handle(BlockedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, notification.UserIds).Select(x => x.Email);

                foreach (var userEmail in usersEmails)
                    {
                        var even = _eventService.EventById(notification.Id);
                        string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";

                        await _sender.SendEmailAsync(new EmailDto
                        {
                            Subject = "Your event was blocked",
                            RecepientEmail = userEmail,
                            MessageText = $"Dear {userEmail}, your event was blocked for some reason. " +
                            $"To unblock it, edit this event, please: " +
                            $"\"<a href='{eventLink}'>{even.Title}</>\"",
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
