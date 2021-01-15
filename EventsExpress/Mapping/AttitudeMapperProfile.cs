using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class AttitudeMapperProfile : Profile
    {
        public AttitudeMapperProfile()
        {
            CreateMap<AttitudeDTO, AttitudeViewModel>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => src.Attitude));

            CreateMap<AttitudeViewModel, AttitudeDTO>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => src.Attitude));

            CreateMap<AttitudeDTO, Relationship>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => (Attitude)src.Attitude))
                .ForMember(dest => dest.UserFrom, opts => opts.Ignore())
                .ForMember(dest => dest.UserTo, opts => opts.Ignore())
                .ForMember(dest => dest.Id, opts => opts.Ignore());
        }
    }
}
