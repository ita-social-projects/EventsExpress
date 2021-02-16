namespace EventsExpress.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Entities;
    using EventsExpress.ViewModels;

    public class NotificationTypeMapperProfile : Profile
    {
        public NotificationTypeMapperProfile()
        {
            CreateMap<NotificationType, NotificationTypeDTO>()
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<NotificationTypeDTO, NotificationTypeViewModel>()
                 .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<NotificationTypeViewModel, NotificationTypeDTO>()
                 .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
