using System;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventsExpress.NotificationHandlers
{
    public class RegisterVerificationHandler : INotificationHandler<RegisterVerificationMessage>
    {
        private readonly IEmailService _sender;
        private readonly ICacheHelper _cacheHelper;
        private readonly ILogger<RegisterVerificationHandler> _logger;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IOptions<AppBaseUrlModel> _urlOptions;

        public RegisterVerificationHandler(
            IEmailService sender,
            ICacheHelper cacheHelper,
            ILogger<RegisterVerificationHandler> logger,
            INotificationTemplateService notificationTemplateService,
            IOptions<AppBaseUrlModel> urlOptions)
        {
            _sender = sender;
            _cacheHelper = cacheHelper;
            _logger = logger;
            _notificationTemplateService = notificationTemplateService;
            _urlOptions = urlOptions;
        }

        public async Task Handle(RegisterVerificationMessage notification, CancellationToken cancellationToken)
        {
            const NotificationProfile profile = NotificationProfile.RegisterVerification;
            var token = Guid.NewGuid().ToString();
            var model = _notificationTemplateService.GetModelByTemplateId<RegisterVerificationNotificationTemplateModel>(profile);

            model.EmailLink = $"<a target=\"_blank\" href=\"{_urlOptions.Value.Host}/authentication/{notification.AuthLocal.Id}/{token}\">link</a>";

            _cacheHelper.Add(new CacheDto
            {
                Key = notification.AuthLocal.Id.ToString(),
                Value = token,
            });

            var templateDto = await _notificationTemplateService.GetByIdAsync(profile);

            try
            {
                await _sender.SendEmailAsync(new EmailDto
                {
                    Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, model),
                    RecepientEmail = notification.AuthLocal.Email,
                    MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, model),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
