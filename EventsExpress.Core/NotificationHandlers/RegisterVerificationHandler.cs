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
using EventsExpress.Core.Infrastructure;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.NotificationHandlers
{
    public class RegisterVerificationHandler :INotificationHandler<RegisterVerificationMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly ICacheHelper _cacheHepler;
        private readonly IOptions<HostSettings> _urlOptions;

        public RegisterVerificationHandler(
            IEmailService sender,
            IUserService userSrv,
            ICacheHelper cacheHepler,
            IOptions<HostSettings> opt
            )
        {
            _sender = sender;
            _userService = userSrv;
            _cacheHepler = cacheHepler;
            _urlOptions = opt;
        }

        public async Task Handle(RegisterVerificationMessage notification, CancellationToken cancellationToken)
        {
            
            Debug.WriteLine("messagehandled");
            var token = Guid.NewGuid().ToString();
            var host = _urlOptions.Value.Host;
            var port = _urlOptions.Value.Port;

            //string theEmailLink= $"{host}:{port}/authentication/{notification.User.Id}/{token}";

            _cacheHepler.Add(new CacheDTO
            {
                UserId = notification.User.Id,
                Token = token
            });

            try
            {
                string theEmailLink = $"<a style=\"color:#0f0ff6; text-decoration:none\" target=\"_blank\" href=\"https://localhost:44376/authentication/{notification.User.Id}/{token}\">link</a>";
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "EventExpress registration",
                    RecepientEmail = notification.User.Email,
                    MessageText = $"For confirm your email please follow the {theEmailLink}"
                });

                var x = _cacheHepler.GetValue(notification.User.Id);

                Debug.WriteLine(x.Token);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
