using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Mapping
{
    public class LocationMapperProfile : Profile
    {
        public LocationMapperProfile()
        {
            CreateMap<LocationDto, EventLocation>().ReverseMap();
            CreateMap<EventDto, LocationDto>()
                .ForMember(dest => dest.OnlineMeeting, opts => opts.MapFrom(src => src.Location.OnlineMeeting))
                .ForMember(dest => dest.Point, opts => opts.MapFrom(src => src.Location.Point))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Location.Type))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
