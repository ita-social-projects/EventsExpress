using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventsExpress.Core.NotificationHandlers
{
    public class CreateEventVerificationHandler : INotificationHandler<CreateEventVerificationMessage>
    {
        private readonly ILogger<CreateEventVerificationHandler> _logger;
        private readonly IEmailService _sender;
        private readonly IUserService _userService;
        private readonly NotificationChange _nameNotification = NotificationChange.OwnEvent;
        private readonly ITrackService _trackService;

        public CreateEventVerificationHandler(
            ILogger<CreateEventVerificationHandler> logger,
            IEmailService sender,
            IUserService userService,
            ITrackService trackService)
        {
            _logger = logger;
            _sender = sender;
            _userService = userService;
            _trackService = trackService;
        }

        public async Task Handle(CreateEventVerificationMessage notification, CancellationToken cancellationToken)
        {
            var changeInfos = await _trackService.GetChangeInfoByScheduleIdAsync(notification.EventSchedule.Id);

            if (changeInfos == null)
            {
                return;
            }

            try
            {
                var usersId = new[] { changeInfos.UserId };
                var userEmail = _userService.GetUsersByNotificationTypes(_nameNotification, usersId).Select(x => x.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    string link = $"{AppHttpContext.AppBaseUrl}/eventSchedule/{notification.EventSchedule.Id}";
                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = "Aprove your reccurent event!",
                        RecepientEmail = userEmail,
                        MessageText = $"Follow the <a href='{link}'>link</a> to create the reccurent event.",
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
