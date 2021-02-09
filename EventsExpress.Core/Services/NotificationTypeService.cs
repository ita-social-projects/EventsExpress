namespace EventsExpress.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Core.Exceptions;
    using EventsExpress.Core.IServices;
    using EventsExpress.Db.BaseService;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;
    using Microsoft.EntityFrameworkCore;

    public class NotificationTypeService : BaseService<NotificationType>, INotificationTypeService
    {
        public NotificationTypeService(AppDbContext context, IMapper mapper)
        : base(context, mapper)
        {
        }

        public IEnumerable<NotificationTypeDTO> GetAllNotificationTypes()
        {
            var notificationTypes = Context.NotificationTypes.Include(c => c.Users).Select(x => new NotificationTypeDTO
            {
                Id = x.Id,
                Name = x.Name,
                CountOfUser = x.Users.Count(),
            })
            .OrderBy(notification => notification.Name);

            return notificationTypes;
        }

        public NotificationType GetById(NotificationChange id)
        {
            return Context.NotificationTypes.Find(id);
        }
    }
}
