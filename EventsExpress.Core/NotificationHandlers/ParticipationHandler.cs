using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.Core.NotificationHandlers
{
    public class ParticipationHandler : INotificationHandler<ParticipationMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.VisitedEvent;
        private readonly INotificationTemplateService _messageService;

        public ParticipationHandler(
            IEmailService sender,
            IUserService userSrv,
            INotificationTemplateService messageService)
        {
            _sender = sender;
            _userService = userSrv;
            _messageService = messageService;
        }

        public async Task Handle(ParticipationMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var usersIds = new[] { notification.UserId };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, usersIds).Select(x => x.Email).SingleOrDefault();

                if (userEmail != null)
                {
                    string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";

                    var notificationTitle = notification.Status.Equals(UserStatusEvent.Approved) ?
                        NotificationProfile.ParticipationApproved
                        : NotificationProfile.ParticipationDenied;

                    var message = await _messageService.GetByIdAsync(notificationTitle);

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                        { "(link)", eventLink },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _messageService.PerformReplacement(message.Subject, pattern),
                        RecepientEmail = userEmail,
                        MessageText = _messageService.PerformReplacement(message.Message, pattern),
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
