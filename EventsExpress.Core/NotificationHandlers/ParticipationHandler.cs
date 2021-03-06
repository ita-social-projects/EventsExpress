﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.NotificationHandlers
{
    public class ParticipationHandler : INotificationHandler<ParticipationMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.VisitedEvent;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IOptions<AppBaseUrlModel> _urlOptions;

        public ParticipationHandler(
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

        public async Task Handle(ParticipationMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var usersIds = new[] { notification.UserId };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, usersIds).Select(x => x.Email).SingleOrDefault();

                if (userEmail != null)
                {
                    string eventLink = $"{_urlOptions.Value.Host}/event/{notification.Id}/1";

                    var templateId = notification.Status.Equals(UserStatusEvent.Approved) ?
                        NotificationProfile.ParticipationApproved
                        : NotificationProfile.ParticipationDenied;

                    var templateDto = await _notificationTemplateService.GetByIdAsync(templateId);

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                        { "(link)", eventLink },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, pattern),
                        RecepientEmail = userEmail,
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
