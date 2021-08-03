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
using EventsExpress.Hubs;
using EventsExpress.Hubs.Clients;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.NotificationHandlers
{
    public class UnblockedUserHandler : INotificationHandler<UnblockedAccountMessage>
    {
        private readonly IHubContext<UsersHub, IUsersClient> _usersHubContext;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly INotificationTemplateService _notificationTemplateService;

        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public UnblockedUserHandler(
            IEmailService sender,
            IUserService userService,
            INotificationTemplateService notificationTemplateService,
            IHubContext<UsersHub, IUsersClient> usersHubContext)
        {
            _usersHubContext = usersHubContext;
            _sender = sender;
            _userService = userService;
            _notificationTemplateService = notificationTemplateService;
        }

        public async Task Handle(UnblockedAccountMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = new[] { notification.Account.UserId.Value };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();

                if (userEmail != null)
                {
                    var templateDto = await _notificationTemplateService.GetByIdAsync(NotificationProfile.UnblockedUser);

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, pattern),
                        RecepientEmail = userEmail,
                        MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, pattern),
                    });
                }

                await _usersHubContext.Clients.All.CountUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}