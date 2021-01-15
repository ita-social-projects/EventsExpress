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
            CreateMap<EventDTO, LocationDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
