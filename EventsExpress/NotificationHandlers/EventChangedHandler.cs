using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Enums;
using Microsoft.Extensions.Options;

namespace EventsExpress.NotificationHandlers
{
    public abstract class EventChangedHandler
    {
        private readonly IEmailService _sender;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IOptions<AppBaseUrlModel> _urlOptions;

        private protected EventChangedHandler(
            IEmailService sender,
            INotificationTemplateService notificationTemplateService,
            IOptions<AppBaseUrlModel> urlOptions)
        {
            _sender = sender;
            _notificationTemplateService = notificationTemplateService;
            _urlOptions = urlOptions;
        }

        private protected async Task SendEmail(NotificationProfile templateId, IEnumerable<string> usersEmails, Guid eventId)
        {
            var templateDto = await _notificationTemplateService.GetByIdAsync(templateId);
            var model = _notificationTemplateService.GetModelByTemplateId<EventChangeNotificationTemplateModel>(templateId);
            foreach (string email in usersEmails)
            {
                model.UserEmail = email;
                model.EventLink = $"{_urlOptions.Value.Host}/event/{eventId}/1";
                await _sender.SendEmailAsync(new EmailDto
                {
                    Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, model),
                    RecepientEmail = email,
                    MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, model),
                });
            }
        }
    }
}
