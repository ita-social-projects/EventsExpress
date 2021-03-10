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
    public class EventStatusHandler : INotificationHandler<EventStatusMessage>
    {
        private readonly IEventService _eventService;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;

        public EventStatusHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService)
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
        }

        public async Task Handle(EventStatusMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, notification.UserIds).Select(x => x.Email);

                foreach (var email in usersEmails)
                {
                    var userEvent = _eventService.EventById(notification.EventId);
                    string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.EventId}/1";
                    string messageText;
                    string subject;
                    if (notification.EventStatus == EventStatus.Canceled)
                    {
                        subject = $"The event you have been joined was canceled";
                        messageText = $"Dear {email}, the event you have been joined was CANCELED. The reason is: {notification.Reason} " +
                                                      $"\"<a href='{eventLink}'>{userEvent.Title}</>\"";
                    }
                    else if (notification.EventStatus == EventStatus.Blocked)
                    {
                        subject = $"The event you have been joined was blocked";
                        messageText = $"Dear {email}, the event you have been joined was BLOCKED. The reason is: {notification.Reason} " +
                                                      $"\"<a href='{eventLink}'>{userEvent.Title}</>\"";
                    }
                    else
                    {
                        subject = $"The event you have been joined was activated";
                        messageText = $"Dear {email}, the event you have been joined was ACTIVATED. The reason is: {notification.Reason} " +
                                                      $"\"<a href='{eventLink}'>{userEvent.Title}</>\"";
                    }

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = subject,
                        RecepientEmail = email,
                        MessageText = messageText,
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
