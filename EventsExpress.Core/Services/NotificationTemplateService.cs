using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class NotificationTemplateService : BaseService<NotificationTemplate>, INotificationTemplateService
    {
        public NotificationTemplateService(AppDbContext context, IMapper mapper = null)
            : base(context, mapper)
        {
        }

        public async Task<NotificationTemplate> AddAsync(NotificationTemplateDto notificationTemplateDto)
        {
            NotificationTemplate notificationTemplate = Mapper.Map<NotificationTemplate>(notificationTemplateDto);
            Insert(notificationTemplate);
            await Context.SaveChangesAsync();

            return notificationTemplate;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            NotificationTemplate email = await GetByIdAsync(id);
            Delete(email);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationTemplate>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task<NotificationTemplate> GetByIdAsync(Guid id)
        {
            return await Context.NotificationTemplates.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<NotificationTemplate> GetByNotificationTypeAsync(string title)
        {
            return await Entities.FirstAsync(e => e.Title.Equals(title));
        }

        public string PerformReplacement(string text, Dictionary<string, string> pattern)
        {
            return pattern.Aggregate(text, (current, element) => current.Replace(element.Key, element.Value));
        }

        public async Task<NotificationTemplate> UpdateAsync(NotificationTemplateDto notificationTemplateDto)
        {
            NotificationTemplate notificationTemplate = Mapper.Map<NotificationTemplate>(notificationTemplateDto);
            notificationTemplate = Update(notificationTemplate);
            await Context.SaveChangesAsync();

            return notificationTemplate;
        }
    }
}
