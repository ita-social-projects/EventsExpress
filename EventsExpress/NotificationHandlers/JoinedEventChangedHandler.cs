using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace EventsExpress.NotificationHandlers
{
    public class JoinedEventChangedHandler : EventChangedHandler, INotificationHandler<JoinedEventMessage>
    {
        private readonly NotificationChange _nameNotification = NotificationChange.JoinedEvent;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public JoinedEventChangedHandler(
            IEmailService sender,
            IUserService userService,
            IEventService eventService,
            INotificationTemplateService notificationTemplateService,
            IOptions<AppBaseUrlModel> urlOptions)
            : base(
                sender,
                notificationTemplateService,
                urlOptions)
        {
            _userService = userService;
            _eventService = eventService;
        }

        public async Task Handle(JoinedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var eventVisitorsIds = _eventService.EventById(notification.EventId).Visitors.Select(it => it.UserId);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, eventVisitorsIds)
                    .Select(x => x.Email);
                const NotificationProfile templateId = NotificationProfile.JoinedEventChanged;
                await SendEmail(templateId, usersEmails, notification.EventId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
