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

        public EventStatusHandler(
            IEmailService sender,
            IEventService eventService)
        {
            _sender = sender;
            _eventService = eventService;
        }

        public async Task Handle(EventStatusMessage notification, CancellationToken cancellationToken)
        {
            var userEvent = _eventService.EventById(notification.EventId);
            var visitors = userEvent.Visitors;
            string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.EventId}/1";

            foreach (var visitor in visitors)
            {
                var email = visitor.User.Email;
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
                    subject = $"The event you have been joined was unblocked";
                    messageText = $"Dear {email}, the event you have been joined was UNBLOCKED. The reason is: {notification.Reason} " +
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
    }
}
