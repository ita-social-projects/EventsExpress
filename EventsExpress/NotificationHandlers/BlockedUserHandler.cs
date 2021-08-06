using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.Services;
using EventsExpress.Db.Enums;
using EventsExpress.Hubs;
using EventsExpress.Hubs.Clients;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.NotificationHandlers
{
    public class BlockedUserHandler : INotificationHandler<BlockedAccountMessage>
    {
        private readonly IHubContext<UsersHub, IUsersClient> _usersHubContext;
        private readonly IEmailService _sender;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public BlockedUserHandler(
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

        public async Task Handle(BlockedAccountMessage notification, CancellationToken cancellationToken)
        {
            var model = notification.Model;

            try
            {
                var userIds = new[] { notification.Account.UserId.Value };
                var userEmail = model.UserName = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();

                if (userEmail != null)
                {
                    var templateDto = await _notificationTemplateService.GetByIdAsync(NotificationProfile.BlockedUser);

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = NotificationTemplateService.PerformReplacement(templateDto.Subject, model),
                        RecepientEmail = userEmail,
                        MessageText = NotificationTemplateService.PerformReplacement(templateDto.Message, model),
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
