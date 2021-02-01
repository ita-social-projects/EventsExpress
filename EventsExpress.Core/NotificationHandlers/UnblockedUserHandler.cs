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
    public class UnblockedUserHandler : INotificationHandler<UnblockedUserMessage>
    {
        private readonly IEmailService _sender;

        public UnblockedUserHandler(
            IEmailService sender)
        {
            _sender = sender;
        }

        public async Task Handle(UnblockedUserMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                await _sender.SendEmailAsync(new EmailDto
                {
                    Subject = "Your account was Unblocked",
                    RecepientEmail = notification.Email,
                    MessageText = $"Dear {notification.Email}, congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress",
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
