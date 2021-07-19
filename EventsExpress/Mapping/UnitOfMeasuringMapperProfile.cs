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
            CreateMap<UnitOfMeasuring, UnitOfMeasuringDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryOfMeasuringDto
             {
                 Id = src.Category.Id,
                 CategoryName = src.Category.CategoryName,
             }));

            CreateMap<UnitOfMeasuringDto, UnitOfMeasuring>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Inventories, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<UnitOfMeasuringDto, UnitOfMeasuringViewModel>()

                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryOfMeasuringViewModel
                 {
                     Id = src.Category.Id,
                     CategoryName = src.Category.CategoryName,
                 }));

            CreateMap<UnitOfMeasuringViewModel, UnitOfMeasuringDto>()
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryOfMeasuringDto
                {
                    Id = src.Id,
                    CategoryName = src.Category.CategoryName,
                }));

            CreateMap<UnitOfMeasuringCreateViewModel, UnitOfMeasuringDto>()
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryOfMeasuringDto
                {
                    Id = src.CategoryId,
                }))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
