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
    public class UnblockedUserHandler : INotificationHandler<UnblockedUserMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEmailMessageService _messageService;

        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public UnblockedUserHandler(
            IEmailService sender,
            IUserService userService,
            IEmailMessageService messageService)
        {
            _sender = sender;
            _userService = userService;
            _messageService = messageService;
        }

        public async Task Handle(UnblockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = new[] { notification.User.Id };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();

                if (userEmail != null)
                {
                    var message = await _messageService.GetByNotificationTypeAsync("UnblockedUser");

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _messageService.PerformReplacement(message.Subject, pattern),
                        RecepientEmail = userEmail,
                        MessageText = _messageService.PerformReplacement(message.MessageText, pattern),
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
