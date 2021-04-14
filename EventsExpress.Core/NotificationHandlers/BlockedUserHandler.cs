using System;
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
    public class BlockedUserHandler : INotificationHandler<BlockedUserMessage>
    {
        private readonly IEmailService _sender;
        private readonly INotificationTemplateService _notificationTemplateServiceService;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public BlockedUserHandler(
            IEmailService sender,
            IUserService userService,
            INotificationTemplateService notificationTemplateService)
        {
            _sender = sender;
            _userService = userService;
            _notificationTemplateServiceService = notificationTemplateService;
        }

        public async Task Handle(BlockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = new[] { notification.User.Id };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                    };

                    var emailMessage = await _notificationTemplateServiceService.GetByIdAsync(NotificationProfile.BlockedUser);

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateServiceService.PerformReplacement(emailMessage.Subject, pattern),
                        RecepientEmail = userEmail,
                        MessageText = _notificationTemplateServiceService.PerformReplacement(emailMessage.MessageText, pattern),
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
