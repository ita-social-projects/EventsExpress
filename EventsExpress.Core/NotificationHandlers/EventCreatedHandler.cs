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
    public class EventCreatedHandler : INotificationHandler<EventCreatedMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly INotificationTemplateService _messageService;

        public EventCreatedHandler(
            IEmailService sender,
            IUserService userSrv,
            INotificationTemplateService messageService)
        {
            _sender = sender;
            _userService = userSrv;
            _messageService = messageService;
        }

        public async Task Handle(EventCreatedMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                var userIds = _userService.GetUsersByCategories(notification.Event.Categories).Select(x => x.Id);
                var usersEmails = _userService.GetUsersByNotificationTypes(_nameNotification, userIds).Select(x => x.Email);

                var templateDto = await _messageService.GetByIdAsync(NotificationProfile.EventCreated);

                foreach (var userEmail in usersEmails)
                {
                    string link = $"{AppHttpContext.AppBaseUrl}/event/{notification.Event.Id}/1";

                    Dictionary<string, string> pattern = new Dictionary<string, string>
                    {
                        { "(UserName)", userEmail },
                        { "(link)", link },
                    };

                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _messageService.PerformReplacement(templateDto.Subject, pattern),
                        RecepientEmail = userEmail,
                        MessageText = _messageService.PerformReplacement(templateDto.Message, pattern),
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
