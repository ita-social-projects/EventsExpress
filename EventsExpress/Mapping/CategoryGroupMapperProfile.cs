using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class CategoryGroupMapperProfile : Profile
    {
        public CategoryGroupMapperProfile()
        {
            CreateMap<CategoryGroupDto, CategoryGroup>()
                .ForMember(dest => dest.Categories, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<CategoryGroupDto, CategoryGroupViewModel>()
                .ReverseMap();
        }
    }
}
