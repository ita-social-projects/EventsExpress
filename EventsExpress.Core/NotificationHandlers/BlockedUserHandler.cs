﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.Core.NotificationHandlers
{
    public class BlockedUserHandler : INotificationHandler<BlockedAccountMessage>
    {
        private readonly IEmailService _sender;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public BlockedUserHandler(
            IEmailService sender,
            IUserService userService,
            INotificationTemplateService notificationTemplateService)
        {
            _sender = sender;
            _userService = userService;
            _notificationTemplateService = notificationTemplateService;
        }

        public async Task Handle(BlockedAccountMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = new[] { notification.Account.UserId.Value };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                    };

                    var templateDto = await _notificationTemplateService.GetByIdAsync(NotificationProfile.BlockedUser);

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
