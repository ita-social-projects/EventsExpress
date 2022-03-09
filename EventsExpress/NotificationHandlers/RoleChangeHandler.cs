using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.NotificationHandlers
{
    public class RoleChangeHandler : INotificationHandler<RoleChangeMessage>
    {
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IUserService _userService;
        private readonly IEmailService _sender;
        private readonly NotificationChange _nameNotification = NotificationChange.Profile;

        public RoleChangeHandler(INotificationTemplateService notificationTemplateService, IUserService userService, IEmailService sender)
        {
            _notificationTemplateService = notificationTemplateService;
            _userService = userService;
            _sender = sender;
        }

        public async Task Handle(RoleChangeMessage notification, CancellationToken cancellationToken)
        {
            try
            {
                const NotificationProfile profile = NotificationProfile.RoleChanged;
                var model = _notificationTemplateService.GetModelByTemplateId<RoleChangeNotificationTemplateModel>(profile);
                var userIds = new[] { notification.Account.UserId.Value };
                model.UserEmail = _userService.GetUsersByNotificationTypes(_nameNotification, userIds)
                    .Select(x => x.Email)
                    .SingleOrDefault();
                model.Roles = notification.Roles;
                if (model.UserEmail != null)
                {
                    var templateDto = await _notificationTemplateService.GetByIdAsync(profile);
                    await _sender.SendEmailAsync(new EmailDto
                    {
                        Subject = _notificationTemplateService.PerformReplacement(templateDto.Subject, model),
                        RecepientEmail = model.UserEmail,
                        MessageText = _notificationTemplateService.PerformReplacement(templateDto.Message, model),
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
