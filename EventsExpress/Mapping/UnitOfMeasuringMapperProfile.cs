using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class UnitOfMeasuringMapperProfile : Profile
    {
        public UnitOfMeasuringMapperProfile()
        {
            CreateMap<UnitOfMeasuring, UnitOfMeasuringDto>().ReverseMap();
            CreateMap<UnitOfMeasuringDto, UnitOfMeasuringViewModel>().ReverseMap();
        }
    }
}
