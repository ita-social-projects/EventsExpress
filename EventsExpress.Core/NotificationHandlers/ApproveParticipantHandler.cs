using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;

namespace EventsExpress.Core.NotificationHandlers
{
    public class ApproveParticipantHandler : INotificationHandler<ApproveParticipantMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public ApproveParticipantHandler(
            IEmailService sender,
            IUserService userService,
            IEventService eventService)
        {
            _sender = sender;
            _userService = userService;
            _eventService = eventService;
        }

        public async Task Handle(ApproveParticipantMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var email = _userService.GetById(notification.UserId).Email;
                var even = _eventService.EventById(notification.Id);
                string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";

                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Approving participants",
                    RecepientEmail = email,
                    MessageText = $"Dear {email}, you have been approved to join to this event. " +
                    $"Enjoy the event with friends." +
                    $"To check it, please, visit EventsExpress: " +
                    $"\"<a href='{eventLink}'>{even.Title}</>\"",
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
