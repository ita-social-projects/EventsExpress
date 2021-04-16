using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class NotificationTemplateMapperProfile : Profile
    {
        public NotificationTemplateMapperProfile()
        {
            CreateMap<NotificationTemplateDTO, NotificationTemplate>()

                // A title is read-only, so we do not need to map this member from DTO, as a result, we save the last value of the title.
                .ForMember(e => e.Title, opts => opts.Ignore())
                .ReverseMap();

            CreateMap<EditNotificationTemplateViewModel, NotificationTemplateDTO>()
                .ReverseMap();
        }
    }
}
