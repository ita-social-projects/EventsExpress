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
    public class OwnEventChangedHandler : EventChangedHandler, INotificationHandler<OwnEventMessage>
    {
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public OwnEventChangedHandler(
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

        public async Task Handle(OwnEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var eventOwnersIds = _eventService.EventById(notification.EventId).Organizers.Select(it => it.Id);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, eventOwnersIds)
                    .Select(x => x.Email);
                const NotificationProfile templateId = NotificationProfile.OwnEventChanged;
                await SendEmail(templateId, usersEmails, notification.EventId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
