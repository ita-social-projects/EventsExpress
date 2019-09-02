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
    public class UnblockedUserHandler : INotificationHandler<UnblockedUserMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;

        public UnblockedUserHandler(
            IEmailService sender,
            IUserService userSrv
            )
        {
            _sender = sender;
            _userService = userSrv;
        }

        public async Task Handle(UnblockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Your account was UNblocked",
                    RecepientEmail = notification.Email,
                    MessageText = $"Dear {notification.Email}, congratulations, your account was UNblocked, so you can come back and enjoy spending you time in EventExpress"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
