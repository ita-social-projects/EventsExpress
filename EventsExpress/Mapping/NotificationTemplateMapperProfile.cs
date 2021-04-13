using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Mapping
{
    public class NotificationTemplateMapperProfile : Profile
    {
        public NotificationTemplateMapperProfile()
        {
            CreateMap<NotificationTemplateDTO, NotificationTemplate>()
                .ReverseMap();
        }
    }
}
