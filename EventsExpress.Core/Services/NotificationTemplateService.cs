using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.NotificationTemplateModels;
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

        private static Dictionary<NotificationProfile, Func<INotificationTemplateModel>> Dependencies { get; } =
            new Dictionary<NotificationProfile, Func<INotificationTemplateModel>>
            {
                { NotificationProfile.BlockedUser, () => new AccountStatusNotificationTemplateModel() },
                { NotificationProfile.EventCreated, () => new EventCreatedNotificationTemplateModel() },
                { NotificationProfile.ParticipationApproved, () => new ParticipationNotificationTemplateModel() },
                { NotificationProfile.ParticipationDenied, () => new ParticipationNotificationTemplateModel() },
                { NotificationProfile.RegisterVerification, () => new RegisterVerificationNotificationTemplateModel() },
                { NotificationProfile.UnblockedUser, () => new AccountStatusNotificationTemplateModel() },
                { NotificationProfile.CreateEventVerification, () => new CreateEventVerificationNotificationTemplateModel() },
                { NotificationProfile.EventStatusActivated, () => new EventStatusNotificationTemplateModel() },
                { NotificationProfile.EventStatusBlocked, () => new EventStatusNotificationTemplateModel() },
                { NotificationProfile.EventStatusCanceled, () => new EventStatusNotificationTemplateModel() },
            };

        private static string AddBraces(string property)
        {
            return $"{{{{{property}}}}}";
        }

        public TModelType GetModelByTemplateId<TModelType>(NotificationProfile id)
            where TModelType : class, INotificationTemplateModel
        {
            return (TModelType)Dependencies
                .Where(d => d.Key == id)
                .Select(d => d.Value)
                .First()
                .Invoke();
        }

        private static Dictionary<string, string> GetPropertiesAndValuesFromObject<T>(T model)
        {
            var type = model.GetType();
            var dictionary = new Dictionary<string, string>();

            foreach (var propInfo in type.GetProperties())
            {
                var key = AddBraces(propInfo.Name);
                var value = propInfo.GetValue(model)?.ToString();

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public string PerformReplacement<T>(string text, T model)
            where T : class, INotificationTemplateModel
        {
            string paramName = default;

            if (text == null)
            {
                paramName = nameof(text);
            }
            else if (model == null)
            {
                paramName = nameof(model);
            }

            if (paramName != default)
            {
                throw new ArgumentNullException(paramName, "parameter can't be null");
            }

            var pattern = GetPropertiesAndValuesFromObject(model);

            return pattern.Aggregate(text, (current, element) => current
                .Replace(element.Key, element.Value));
        }

        public IEnumerable<string> GetModelPropertiesByTemplateId(NotificationProfile id)
        {
            var model = this.GetModelByTemplateId<INotificationTemplateModel>(id);

            return model.GetType()
                .GetProperties()
                .Select(prop => AddBraces(prop.Name));
        }

        public async Task<IEnumerable<NotificationTemplateDto>> GetAllAsync()
        {
            var templatesDto = Mapper.Map<IEnumerable<NotificationTemplateDto>>(await Entities.ToListAsync());
            return templatesDto;
        }

        public async Task<NotificationTemplateDto> GetByIdAsync(NotificationProfile id)
        {
            var template = await Context.NotificationTemplates.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (template == null)
            {
                throw new EventsExpressException("The notification template does not exist");
            }

            return Mapper.Map<NotificationTemplateDto>(template);
        }

        public async Task UpdateAsync(NotificationTemplateDto notificationTemplateDto)
        {
            var notificationTemplate = await Context.NotificationTemplates
                .FirstOrDefaultAsync(e => e.Id.Equals(notificationTemplateDto.Id));

            Mapper.Map(notificationTemplateDto, notificationTemplate);

            Update(notificationTemplate);
            await Context.SaveChangesAsync();
        }
    }
}
