using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfEvents, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryCreateViewModel, CategoryDto>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfEvents, opts => opts.Ignore());
            CreateMap<CategoryEditViewModel, CategoryDto>()
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfEvents, opts => opts.Ignore());
        }
    }
}
