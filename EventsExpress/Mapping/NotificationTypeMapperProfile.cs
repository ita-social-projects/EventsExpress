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
            CreateMap<NotificationType, NotificationTypeDto>()
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<NotificationTypeDto, NotificationTypeViewModel>()
                 .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<NotificationTypeViewModel, NotificationTypeDto>()
                 .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
