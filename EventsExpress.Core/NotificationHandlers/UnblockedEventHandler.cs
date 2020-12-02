using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;

namespace EventsExpress.Core.NotificationHandlers
{
    public class UnblockedEventHandler : INotificationHandler<UnblockedEventMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public UnblockedEventHandler(
            IEmailService sender,
            IUserService userSrv,
            IEventService eventService)
        {
            _sender = sender;
            _userService = userSrv;
            _eventService = eventService;
        }

        public async Task Handle(UnblockedEventMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var userId in notification.UserId)
                {
                    var email = _userService.GetById(userId).Email;
                    var even = _eventService.EventById(notification.Id);
                    string link = $"{AppHttpContext.AppBaseUrl}/event/{notification.Id}/1";

                    await _sender.SendEmailAsync(new EmailDTO
                    {
                        Subject = "Your event was Unblocked",
                        RecepientEmail = email,
                        MessageText = $"Dear {email}, congratulations, your event was Unblocked! " +
                        $"\"<a href='{link}'>{even.Title}</>\"",
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
