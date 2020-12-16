using System;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.EF;
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
        protected readonly AppDbContext _context;

        public CreateEventVerificationHandler(
            ILogger<CreateEventVerificationHandler> logger,
            IEmailService sender,
            IUserService userService,
            AppDbContext context)
        {
            _logger = logger;
            _sender = sender;
            _userService = userService;
            _context = context;
        }

        public async Task Handle(CreateEventVerificationMessage notification, CancellationToken cancellationToken)
        {
            var changeInfos = await _context.ChangeInfos
              .FromSqlRaw(@"SELECT * FROM ChangeInfos WHERE EntityName = 'EventSchedule' AND JSON_VALUE(EntityKeys, '$.Id') = '" + notification.EventSchedule.Id + "' AND ChangesType = 2").FirstOrDefaultAsync();

            if (changeInfos.UserId == Guid.Empty)
            {
                return;
            }

            var user = _userService.GetById(changeInfos.UserId);

            try
            {
                string link = $"{AppHttpContext.AppBaseUrl}/eventSchedule/{notification.EventSchedule.Id}";
                await _sender.SendEmailAsync(new EmailDTO
                {
                    Subject = "Aprove your reccurent event!",
                    RecepientEmail = user.Email,
                    MessageText = $"Follow the <a href='{link}'>link</a> to create the reccurent event.",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
