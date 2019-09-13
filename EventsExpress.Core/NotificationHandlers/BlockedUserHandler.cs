using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsExpress.Core.NotificationHandlers
{
    public class BlockedUserHandler : INotificationHandler<BlockedUserMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;

        public BlockedUserHandler(
            IEmailService sender,
            IUserService userSrv
            )
        {
            _sender = sender;
            _userService = userSrv;
        }

        public async Task Handle(BlockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                    await _sender.SendEmailAsync(new EmailDTO
                    {
                        Subject = "Your account was blocked",
                        RecepientEmail = notification.Email,
                        MessageText = $"Dear {notification.Email}, your account was blocked for some reason!"
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
