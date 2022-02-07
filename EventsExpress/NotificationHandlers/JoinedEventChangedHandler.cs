using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace EventsExpress.NotificationHandlers
{
   public class JoinedEventChangedHandler : INotificationHandler<JoinedEventMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly NotificationChange _nameNotification = NotificationChange.JoinedEvent;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IOptions<AppBaseUrlModel> _urlOptions;

        public JoinedEventChangedHandler(
            IEmailService sender,
            IUserService userService,
            IEventService eventService,
            INotificationTemplateService notificationTemplateService,
            IOptions<AppBaseUrlModel> urlOptions)
        {
            _sender = sender;
            _userService = userService;
            _eventService = eventService;
            _notificationTemplateService = notificationTemplateService;
            _urlOptions = urlOptions;
        }

        public async Task Handle(JoinedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var eventVisitorsIds = _eventService.EventById(notification.EventId).Visitors.Select(it => it.UserId);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, eventVisitorsIds)
                    .Select(x => x.Email);
                const NotificationProfile templateId = NotificationProfile.JoinedEventChanged;
                var templateDto = await _notificationTemplateService.GetByIdAsync(templateId);
                var model = _notificationTemplateService.GetModelByTemplateId<EventChangeNotificationTemplateModel>(templateId);
                foreach (string email in usersEmails)
                {
                    model.UserEmail = email;
                    model.EventLink = $"{_urlOptions.Value.Host}/event/{notification.EventId}/1";
                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, model),
                        RecepientEmail = email,
                        MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, model),
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
