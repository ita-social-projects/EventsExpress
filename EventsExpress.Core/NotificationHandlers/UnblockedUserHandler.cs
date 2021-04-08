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
    public class UnblockedUserHandler : INotificationHandler<UnblockedAccountMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;

        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public UnblockedUserHandler(
            IEmailService sender,
            IUserService userService)
        {
            _sender = sender;
            _userService = userService;
        }

        public async Task Handle(UnblockedAccountMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = new[] { notification.Account.UserId.Value };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = "Your account was Unblocked",
                        RecepientEmail = userEmail,
                        MessageText = $"Dear {userEmail}, congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress",
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
