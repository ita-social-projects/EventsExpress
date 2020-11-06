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
    public class DenyParticipantHandler : INotificationHandler<DenyParticipantMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public DenyParticipantHandler(
            IEmailService sender,
            IUserService userService,
            IEventService eventService)
        {
            _sender = sender;
            _userService = userService;
            _eventService = eventService;
        }

        public async Task Handle(DenyParticipantMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var email = _userService.GetById(notification.UserId).Email;
                var even = _eventService.EventById(notification.Id);
                string eventLink = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";

                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Denying participants",
                    RecepientEmail = email,
                    MessageText = $"Dear {email}, you have been denied to join to this event. " +
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
