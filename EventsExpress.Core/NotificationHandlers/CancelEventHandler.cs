using System;
using System.Collections.Generic;
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
    public class CancelEventHandler : INotificationHandler<CancelEventMessage>
    {
        private readonly IEventService _eventService;
        private readonly IEmailService _sender;
        private readonly IEventStatusHistoryService _eventStatusHistoryService;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.VisitedEvent;

        public CancelEventHandler(
            IEmailService sender,
            IEventService eventService,
            IEventStatusHistoryService eventStatusHistoryService,
            IUserService userService)
        {
            _sender = sender;
            _eventService = eventService;
            _eventStatusHistoryService = eventStatusHistoryService;
            _userService = userService;
        }

        public async Task Handle(CancelEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userEvent = _eventService.EventById(notification.EventId);
                var usersIds = userEvent.Visitors.Select(visitor => visitor.UserId);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, usersIds).Select(x => x.Email);
                string reason = _eventStatusHistoryService.GetLastRecord(notification.EventId, EventStatus.Cancelled).Reason;
                string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.EventId}/1";
                foreach (var userEmail in usersEmails)
                    {
                        await _sender.SendEmailAsync(new EmailDto
                        {
                            Subject = $"The event you have been joined was canceled",
                            RecepientEmail = userEmail,
                            MessageText = $"Dear {userEmail}, the event you have been joined was canceled. The reason is: {reason} " +
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
