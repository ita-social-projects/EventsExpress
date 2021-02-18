using System;
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

        public ParticipationHandler(
            IEmailService sender,
            IUserService userSrv)
        {
            _sender = sender;
            _userService = userSrv;
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
                    string message = notification.Status == UserStatusEvent.Approved
                        ? "you have been approved to join to this event."
                        : "you have been denied to join to this event.";

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = notification.Status == UserStatusEvent.Approved
                            ? "Approving participation"
                            : "Denying participation",
                        RecepientEmail = userEmail,
                        MessageText = $"Dear {userEmail}, " +
                        message +
                        $"To check it, please, visit " +
                        $"\"<a href='{eventLink}'>EventExpress</>\"",
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
