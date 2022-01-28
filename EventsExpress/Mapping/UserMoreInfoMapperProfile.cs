namespace EventsExpress.Mapping
{
    using System.Linq;
    using AutoMapper;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels;

    public class UserMoreInfoMapperProfile : Profile
    {
        public UserMoreInfoMapperProfile()
        {
            CreateMap<UserMoreInfoDto, UserMoreInfoCreateViewModel>()
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ParentStatus, opts => opts.MapFrom(src => src.ParentStatus))
                .ForMember(dest => dest.ReasonsForUsingTheSite, opts => opts.MapFrom(src => src.ReasonsForUsingTheSite))
                .ForMember(dest => dest.EventTypes, opts => opts.MapFrom(src => src.EventTypes))
                .ForMember(dest => dest.RelationShipStatus, opts => opts.MapFrom(src => src.RelationShipStatus))
                .ForMember(dest => dest.TheTypeOfLeisure, opts => opts.MapFrom(src => src.TheTypeOfLeisure))
                .ForMember(dest => dest.AditionalInfoAboutUser, opts => opts.MapFrom(src => src.AditionalInfoAboutUser))
                .ForSourceMember(src => src.Id, opts => opts.DoNotValidate());

            CreateMap<UserMoreInfoCreateViewModel, UserMoreInfoDto>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ParentStatus, opts => opts.MapFrom(src => src.ParentStatus))
                .ForMember(dest => dest.ReasonsForUsingTheSite, opts => opts.MapFrom(src => src.ReasonsForUsingTheSite))
                .ForMember(dest => dest.EventTypes, opts => opts.MapFrom(src => src.EventTypes))
                .ForMember(dest => dest.RelationShipStatus, opts => opts.MapFrom(src => src.RelationShipStatus))
                .ForMember(dest => dest.TheTypeOfLeisure, opts => opts.MapFrom(src => src.TheTypeOfLeisure))
                .ForMember(dest => dest.AditionalInfoAboutUser, opts => opts.MapFrom(src => src.AditionalInfoAboutUser));

            CreateMap<UserMoreInfoDto, UserMoreInfo>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ParentStatus, opts => opts.MapFrom(src => src.ParentStatus))
                .ForMember(dest => dest.ReasonsForUsingTheSite, opts => opts.MapFrom(src => src.ReasonsForUsingTheSite.Select(x => new UserMoreInfoReasonsForUsingTheSite() { UserMoreInfoId = src.Id, ReasonsForUsingTheSite = x, Id = System.Guid.NewGuid() }).ToList()))
                .ForMember(dest => dest.EventTypes, opts => opts.MapFrom(src => src.EventTypes.Select(x => new UserMoreInfoEventType() { UserMoreInfoId = src.Id, EventType = x, Id = System.Guid.NewGuid() }).ToList()))
                .ForMember(dest => dest.RelationShipStatus, opts => opts.MapFrom(src => src.RelationShipStatus))
                .ForMember(dest => dest.TheTypeOfLeisure, opts => opts.MapFrom(src => src.TheTypeOfLeisure))
                .ForMember(dest => dest.AditionalInfoAboutUser, opts => opts.MapFrom(src => src.AditionalInfoAboutUser));
        }
    }
}
