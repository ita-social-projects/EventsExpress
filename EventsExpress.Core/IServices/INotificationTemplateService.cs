using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface INotificationTemplateService
    {
        public Task<NotificationTemplateDTO> AddAsync(NotificationTemplateDTO notificationTemplateDto);

        public Task<IEnumerable<NotificationTemplateDTO>> GetAllAsync();

        public Task<IEnumerable<NotificationTemplateDTO>> GetAsync(int page, int pageSize);

        public Task<NotificationTemplateDTO> GetByIdAsync(Guid id);

        public Task<NotificationTemplateDTO> GetByTitleAsync(string title);

        public Task DeleteByIdAsync(Guid id);

        public Task<NotificationTemplateDTO> UpdateAsync(NotificationTemplateDTO notificationTemplateDto);

        public string PerformReplacement(string text, Dictionary<string, string> pattern);
    }
}
