using System;
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
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public BlockedUserHandler(
            IEmailService sender, IUserService userService)
        {
            _sender = sender;
            _userService = userService;
        }

        public async Task Handle(BlockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = new[] { notification.User.Id };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = "Your account was blocked",
                        RecepientEmail = userEmail,
                        MessageText = $"Dear {userEmail}, your account was blocked for some reason!",
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
