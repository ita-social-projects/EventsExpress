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
        private IEventService _eventService;
        private IEmailService _sender;

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
            string subject = notification.EventStatus == EventStatus.Canceled
                ? $"The event you have been joined was canceled"
                : $"The event you have been joined was uncanceled";

            foreach (var visitor in visitors)
            {
                var email = visitor.User.Email;
                string messageText = notification.EventStatus == EventStatus.Canceled
                                ? $"Dear {email}, the event you have been joined was CANCELED. The reason is: {notification.Reason} " +
                                                  $"\"<a href='{eventLink}'>{userEvent.Title}</>\""
                                : $"Dear {email}, the event you have been joined was UNCANCELED. The reason is: {notification.Reason} " +
                                                  $"\"<a href='{eventLink}'>{userEvent.Title}</>\"";

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
