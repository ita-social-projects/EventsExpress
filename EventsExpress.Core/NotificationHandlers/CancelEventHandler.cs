using System;
using System.Diagnostics;
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
    public class CancelEventHandler : INotificationHandler<CancelEventMessage>
    {
        private IEventService _eventService;
        private IEmailService _sender;
        private IEventStatusHistoryService _eventStatusHistoryService;

        public CancelEventHandler(
            IEmailService sender,
            IEventService eventService,
            IEventStatusHistoryService eventStatusHistoryService)
        {
            _sender = sender;
            _eventService = eventService;
            _eventStatusHistoryService = eventStatusHistoryService;
        }

        public async Task Handle(CancelEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userEvent = _eventService.EventById(notification.EventId);
                var visitors = userEvent.Visitors;
                string reason = _eventStatusHistoryService.GetLastRecord(notification.EventId, EventStatus.Cancelled).Reason;
                string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.EventId}/1";
                foreach (var visitor in visitors)
                {
                    var email = visitor.User.Email;
                    await _sender.SendEmailAsync(new EmailDTO
                    {
                        Subject = $"The event you have been joined was canceled",
                        RecepientEmail = email,
                        MessageText = $"Dear {email}, the event you have been joined was canceled. The reason is: {reason} " +
                                      $"\"<a href='{eventLink}'>{userEvent.Title}</>\"",
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
