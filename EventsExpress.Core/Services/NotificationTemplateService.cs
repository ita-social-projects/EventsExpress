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

        public async Task<NotificationTemplateDTO> AddAsync(NotificationTemplateDTO notificationTemplateDto)
        {
            NotificationTemplate notificationTemplate = Mapper.Map<NotificationTemplate>(notificationTemplateDto);
            Insert(notificationTemplate);
            await Context.SaveChangesAsync();

            notificationTemplateDto = await GetByTitleAsync(notificationTemplate.Title);

            return notificationTemplateDto;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var template = Mapper.Map<NotificationTemplate>(GetByIdAsync(id));
            Delete(template);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationTemplateDTO>> GetAllAsync()
        {
            var templates = await Entities.ToListAsync();
            return Mapper.Map<IEnumerable<NotificationTemplateDTO>>(templates);
        }

        public async Task<IEnumerable<NotificationTemplateDTO>> GetAsync(int pageNumber, int pageSize)
        {
            var templates = await Entities.OrderBy(e => e.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var templatesDto = Mapper.Map<IEnumerable<NotificationTemplateDTO>>(templates);

            return templatesDto;
        }

        public async Task<NotificationTemplateDTO> GetByIdAsync(Guid id)
        {
            var template = await Context.NotificationTemplates.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return Mapper.Map<NotificationTemplateDTO>(template);
        }

        public async Task<NotificationTemplateDTO> GetByTitleAsync(string title)
        {
            var template = await Entities.AsNoTracking()
                .FirstAsync(e => e.Title.Equals(title));
            return Mapper.Map<NotificationTemplateDTO>(template);
        }

        public string PerformReplacement(string text, Dictionary<string, string> pattern)
        {
            return pattern.Aggregate(text, (current, element) => current.Replace(element.Key, element.Value));
        }

        public async Task<NotificationTemplateDTO> UpdateAsync(NotificationTemplateDTO notificationTemplateDto)
        {
            NotificationTemplate notificationTemplate = Mapper.Map<NotificationTemplate>(notificationTemplateDto);
            notificationTemplate = Update(notificationTemplate);
            await Context.SaveChangesAsync();

            return notificationTemplateDto;
        }
    }
}
