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
    public class OwnEventChangedHandler : INotificationHandler<OwnEventMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IEventService _eventService;

        public OwnEventChangedHandler(
            IEmailService sender,
            IUserService userService,
            IEventService eventService,
            INotificationTemplateService notificationTemplateService)
        {
            _sender = sender;
            _userService = userService;
            _notificationTemplateService = notificationTemplateService;
            _eventService = eventService;
        }

        public async Task Handle(OwnEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var eventOwnersIds = _eventService.EventById(notification.EventId).Owners.Select(it => it.Id);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, eventOwnersIds)
                    .Select(x => x.Email);

                const NotificationProfile templateId = NotificationProfile.OwnEventChanged;
                var templateDto = await _notificationTemplateService.GetByIdAsync(templateId);
                var model = _notificationTemplateService.GetModelByTemplateId<EventChangeNotificationTemplateModel>(templateId);
                foreach (string email in usersEmails)
                {
                    model.UserEmail = email;
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
