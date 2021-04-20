using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.IBaseService;

namespace EventsExpress.Core.IServices
{
    public interface INotificationTemplateService : IBaseService<NotificationTemplate>
    {
        public Task<IEnumerable<NotificationTemplateDto>> GetAllAsync();

        public Task<NotificationTemplateDto> GetByIdAsync(NotificationProfile id);

        public Task UpdateAsync(NotificationTemplateDto notificationTemplateDto);

        public string PerformReplacement(string text, Dictionary<string, string> pattern);
    }
}
