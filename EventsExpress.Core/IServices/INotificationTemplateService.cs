using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.IBaseService;

namespace EventsExpress.Core.IServices
{
    public interface INotificationTemplateService : IBaseService<NotificationTemplate>
    {
        public string PerformReplacement<T>(string text, T model)
            where T : class, INotificationTemplateModel;

        public IEnumerable<string> GetModelPropertiesByTemplateId(NotificationProfile id);

        public TModelType GetModelByTemplateId<TModelType>(NotificationProfile id)
            where TModelType : class, INotificationTemplateModel;

        public Task<IEnumerable<NotificationTemplateDto>> GetAllAsync();

        public Task<NotificationTemplateDto> GetByIdAsync(NotificationProfile id);

        public Task UpdateAsync(NotificationTemplateDto notificationTemplateDto);
    }
}
