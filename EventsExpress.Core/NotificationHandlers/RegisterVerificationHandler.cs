using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.Extensions;

namespace EventsExpress.Core.NotificationHandlers
{
    public class RegisterVerificationHandler :INotificationHandler<RegisterVerificationMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly ICacheHelper _cacheHepler;
      
        public RegisterVerificationHandler
            (
            IEmailService sender,
            IUserService userSrv,
            ICacheHelper cacheHepler
           
            )
        {
            _sender = sender;
            _userService = userSrv;
            _cacheHepler = cacheHepler;
            

        }
        public async Task Handle(RegisterVerificationMessage notification, CancellationToken cancellationToken)
        {
            var token = Guid.NewGuid().ToString();
            string theEmailLink = $"<a \" target=\"_blank\" href=\"{AppHttpContext.AppBaseUrl}/authentication/{notification.User.Id}/{token}\">link</a>";
               
            
            _cacheHepler.Add(new CacheDTO
            {
                UserId = notification.User.Id,
                Token = token
            });

            try
            {
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "EventExpress registration",
                    RecepientEmail = notification.User.Email,
                    MessageText = $"For  confirm your email please follow the {theEmailLink}   "
                });

                var x = _cacheHepler.GetValue(notification.User.Id);

            }
            catch
            {
               
            }
        }
    }
}
