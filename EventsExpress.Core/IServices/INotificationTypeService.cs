namespace EventsExpress.Core.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;

    public interface INotificationTypeService
    {
        IEnumerable<NotificationTypeDto> GetAllNotificationTypes();

        NotificationType GetById(NotificationChange id);
    }
}
