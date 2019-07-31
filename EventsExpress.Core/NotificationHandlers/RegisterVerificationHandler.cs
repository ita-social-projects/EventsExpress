using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EventsExpress.Core.NotificationHandlers
{
    public class RegisterVerificationHandler :INotificationHandler<RegisterVerificationMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;

        public RegisterVerificationHandler(
            IEmailService sender,
            IUserService userSrv
            )
        {
            _sender = sender;
            _userService = userSrv;
        }

        public async Task Handle(RegisterVerificationMessage notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("messagehandled");
            var token = new Guid().ToString();
            try
            {
                await _sender.SendEmailAsync(new EmailDTO {
                    SenderEmail="noreply@EventExpress.com",
                    //RecepientEmail=user.Email,
                   // MessageText=notification.Message
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
