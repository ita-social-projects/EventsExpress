using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class CategoryOfMeasuringMapperProfile : Profile
    {
        public CategoryOfMeasuringMapperProfile()
        {
            CreateMap<CategoryOfMeasuring, CategoryOfMeasuringDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnitOfMeasuring, opt => opt.MapFrom(src => src.UnitOfMeasurings));

            CreateMap<CategoryOfMeasuringDto, CategoryOfMeasuring>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnitOfMeasurings, opt => opt.MapFrom(src => src.UnitOfMeasuring));

            CreateMap<CategoryOfMeasuringDto, CategoryOfMeasuringViewModel>()
                  .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnitOfMeasuring, opt => opt.MapFrom(src => src.UnitOfMeasuring));

            CreateMap<CategoryOfMeasuringViewModel, CategoryOfMeasuringDto>()
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnitOfMeasuring, opt => opt.MapFrom(src => src.UnitOfMeasuring));
        }
    }
}
