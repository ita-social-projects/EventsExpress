using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;

namespace EventsExpress.Core.NotificationHandlers
{
    public class BlockedUserHandler : INotificationHandler<BlockedUserMessage>
    {
        private readonly IEmailService _sender;

        public BlockedUserHandler(
            IEmailService sender)
        {
            _sender = sender;
        }

        public async Task Handle(BlockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Your account was blocked",
                    RecepientEmail = notification.Email,
                    MessageText = $"Dear {notification.Email}, your account was blocked for some reason!",
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
