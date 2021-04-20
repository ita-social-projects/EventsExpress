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
    public class EventStatusHandler : INotificationHandler<EventStatusMessage>
    {
        private readonly IEventService _eventService;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly INotificationTemplateService _notificationTemplateService;

        public EventStatusHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService,
            INotificationTemplateService notificationTemplateService)
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
            _notificationTemplateService = notificationTemplateService;
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

                    var templateId = notification.EventStatus switch
                    {
                        EventStatus.Canceled => NotificationProfile.EventStatusCanceled,
                        EventStatus.Blocked => NotificationProfile.EventStatusBlocked,
                        _ => NotificationProfile.EventStatusActivated
                    };

                    var templateDto = await _notificationTemplateService.GetByIdAsync(templateId);

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", email },
                        { "(link)", eventLink },
                        { "(title)", userEvent.Title },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, pattern),
                        RecepientEmail = email,
                        MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, pattern),
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
