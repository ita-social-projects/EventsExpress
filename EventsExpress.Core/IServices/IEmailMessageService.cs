using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface IEmailMessageService
    {
        public Task<EmailMessage> AddAsync(EmailDto emailDTO, string shortName);

        public Task<IEnumerable<EmailMessage>> GetAllAsync();

        public Task<EmailMessage> GetByIdAsync(Guid id);

        public Task<EmailMessage> GetByNotificationTypeAsync(string notificationType);

        public Task DeleteByIdAsync(Guid id);

        public Task<EmailMessage> UpdateAsync(Guid id, EmailDto emailDTO, string shortName);

        public string PerformReplacement(string text, Dictionary<string, string> pattern);
    }
}
