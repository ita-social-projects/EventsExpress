using System;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventsExpress.NotificationHandlers
{
    public class CreateEventVerificationHandler : INotificationHandler<CreateEventVerificationMessage>
    {
        private readonly ILogger<CreateEventVerificationHandler> _logger;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly ITrackService _trackService;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IOptions<AppBaseUrlModel> _urlOptions;

        public CreateEventVerificationHandler(
            ILogger<CreateEventVerificationHandler> logger,
            IEmailService sender,
            IUserService userService,
            ITrackService trackService,
            INotificationTemplateService notificationTemplateService,
            IOptions<AppBaseUrlModel> urlOptions)
        {
            _logger = logger;
            _sender = sender;
            _userService = userService;
            _trackService = trackService;
            _notificationTemplateService = notificationTemplateService;
            _urlOptions = urlOptions;
        }

        public async Task Handle(CreateEventVerificationMessage notification, CancellationToken cancellationToken)
        {
            const NotificationProfile profile = NotificationProfile.CreateEventVerification;
            var model = _notificationTemplateService.GetModelByTemplateId<CreateEventVerificationNotificationTemplateModel>(profile);
            var changeInfos = await _trackService.GetChangeInfoByScheduleIdAsync(notification.EventSchedule.Id);

            if (changeInfos == null)
            {
                return;
            }

            try
            {
                var usersId = new[] { changeInfos.UserId };
                model.UserEmail = _userService.GetUsersByNotificationTypes(_nameNotification, usersId)
                    .Select(x => x.Email)
                    .SingleOrDefault();

                var templateDto = await _notificationTemplateService.GetByIdAsync(profile);

                if (model.UserEmail != null)
                {
                    model.EventScheduleLink = $"{_urlOptions.Value.Host}/eventSchedule/{notification.EventSchedule.Id}";

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, model),
                        RecepientEmail = model.UserEmail,
                        MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, model),
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
