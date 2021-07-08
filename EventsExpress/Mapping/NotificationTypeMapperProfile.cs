using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class NotificationTypeMapperProfile : Profile
    {
        public NotificationTypeMapperProfile()
        {
            CreateMap<NotificationType, NotificationTypeDto>()
                .ReverseMap();
            CreateMap<NotificationTypeDto, NotificationTypeViewModel>()
                 .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<NotificationTypeViewModel, NotificationTypeDto>()
                 .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<UserNotificationType, NotificationTypeDto>()
                            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.NotificationType.Id))
                            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.NotificationType.Name));
        }
    }
}
