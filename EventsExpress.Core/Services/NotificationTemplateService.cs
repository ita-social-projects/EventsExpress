using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class NotificationTemplateService : BaseService<NotificationTemplate>, INotificationTemplateService
    {
        public NotificationTemplateService(AppDbContext context, IMapper mapper = null)
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<NotificationTemplateDTO>> GetAllAsync()
        {
            var templatesDto = Mapper.Map<IEnumerable<NotificationTemplateDTO>>(await Entities.ToListAsync());
            return templatesDto;
        }

        public async Task<NotificationTemplateDTO> GetByIdAsync(NotificationProfile id)
        {
            var template = await Context.NotificationTemplates.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return Mapper.Map<NotificationTemplateDTO>(template);
        }

        public string PerformReplacement(string text, Dictionary<string, string> pattern)
        {
            return pattern.Aggregate(text, (current, element) => current
                .Replace(element.Key, element.Value));
        }

        public async Task UpdateAsync(NotificationTemplateDTO notificationTemplateDto)
        {
            var notificationTemplate = await Context.NotificationTemplates
                .FirstOrDefaultAsync(e => e.Id.Equals(notificationTemplateDto.Id));

            Mapper.Map(notificationTemplateDto, notificationTemplate);

            Update(notificationTemplate);
            await Context.SaveChangesAsync();
        }
    }
}
