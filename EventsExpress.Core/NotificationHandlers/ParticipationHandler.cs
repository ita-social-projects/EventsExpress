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

        public ParticipationHandler(
            IEmailService sender,
            IUserService userSrv)
        {
            _sender = sender;
            _userService = userSrv;
        }

        public async Task Handle(ParticipationMessage notification, CancellationToken cancellationToken)
        {
            var email = _userService.GetById(notification.UserId).Email;
            string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";
            string message = notification.Status == UserStatusEvent.Approved
                ? "you have been approved to join to this event."
                : "you have been denied to join to this event.";

            await _sender.SendEmailAsync(new EmailDTO
            {
                Subject = notification.Status == UserStatusEvent.Approved
                    ? "Approving participation"
                    : "Denying participation",
                RecepientEmail = email,
                MessageText = $"Dear {email}, " + 
                message +
                $"To check it, please, visit " +
                $"\"<a href='{eventLink}'>EventExpress</>\"",
            });
        }
    }
}
