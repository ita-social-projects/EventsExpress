using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class TrackMapperProfile : Profile
    {
        public TrackMapperProfile()
        {
            CreateMap<ChangeInfo, TrackDto>();
            CreateMap<TrackDto, TrackViewModel>();
        }
    }
}
