using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface INotificationTemplateService
    {
        public Task<NotificationTemplate> AddAsync(NotificationTemplateDto notificationTemplateDto);

        public Task<IEnumerable<NotificationTemplate>> GetAllAsync();

        public Task<NotificationTemplate> GetByIdAsync(Guid id);

        public Task<NotificationTemplate> GetByNotificationTypeAsync(string title);

        public Task DeleteByIdAsync(Guid id);

        public Task<NotificationTemplate> UpdateAsync(NotificationTemplateDto notificationTemplateDto);

        public string PerformReplacement(string text, Dictionary<string, string> pattern);
    }
}
