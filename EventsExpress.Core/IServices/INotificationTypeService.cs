using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface INotificationTypeService
    {
        IEnumerable<NotificationTypeDto> GetAllNotificationTypes();

        NotificationType GetById(NotificationChange id);
    }
}
