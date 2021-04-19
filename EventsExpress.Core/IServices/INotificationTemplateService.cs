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
        public Task<IEnumerable<NotificationTemplateDTO>> GetAllAsync();

        public Task<NotificationTemplateDTO> GetByIdAsync(NotificationProfile id);

        public Task UpdateAsync(NotificationTemplateDTO notificationTemplateDto);

        public string PerformReplacement(string text, Dictionary<string, string> pattern);
    }
}
