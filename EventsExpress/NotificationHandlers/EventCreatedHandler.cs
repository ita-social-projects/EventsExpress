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
    public class EventCreatedHandler : INotificationHandler<EventCreatedMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IOptions<AppBaseUrlModel> _urlOptions;

        public EventCreatedHandler(
            IEmailService sender,
            IUserService userSrv,
            INotificationTemplateService notificationTemplateService,
            IOptions<AppBaseUrlModel> urlOptions)
        {
            _sender = sender;
            _userService = userSrv;
            _notificationTemplateService = notificationTemplateService;
            _urlOptions = urlOptions;
        }

        public async Task Handle(EventCreatedMessage notification, CancellationToken cancellationToken)
        {
            const NotificationProfile profile = NotificationProfile.EventCreated;
            var model = _notificationTemplateService.GetModelByTemplateId<EventCreatedNotificationTemplateModel>(profile);

            try
            {
                var userIds = _userService.GetUsersByCategories(notification.Event.Categories)
                    .Select(x => x.Id);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, userIds)
                    .Select(x => x.Email);
                var templateDto = await _notificationTemplateService.GetByIdAsync(profile);

                model.EventLink = $"{_urlOptions.Value.Host}/event/{notification.Event.Id}/1";

                foreach (var email in usersEmails)
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
