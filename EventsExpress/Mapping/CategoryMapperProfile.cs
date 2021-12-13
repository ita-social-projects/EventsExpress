using System.Linq;
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
                .ForMember(dest => dest.CountOfUser, opts => opts.MapFrom(src => src.Users.Count()))
                .ForMember(dest => dest.CountOfEvents, opts => opts.MapFrom(src => src.Events.Count()))
                .ForMember(dest => dest.CategoryGroup, opts => opts.MapFrom(src =>
                    MapCategoryGroupDtoFromCategoryGroup(src.CategoryGroup)));

            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.Users, opts => opts.Ignore())
                .ForMember(dest => dest.Events, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryGroup, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryGroupId, opts => opts.MapFrom(src => src.CategoryGroup.Id));

            CreateMap<CategoryDto, CategoryViewModel>()
                .ForMember(dest => dest.CategoryGroup, opts => opts.MapFrom(src =>
                    MapCategoryGroupViewModelFromCategoryGroupDto(src.CategoryGroup)));

            CreateMap<CategoryViewModel, CategoryDto>()
                .ForMember(dest => dest.CategoryGroup, opts => opts.MapFrom(src =>
                    MapCategoryGroupDtoFromCategoryGroupViewModel(src.CategoryGroup)));

            CreateMap<CategoryCreateViewModel, CategoryDto>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfEvents, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryGroup, opts => opts.MapFrom(src =>
                    MapCategoryGroupDtoFromCategoryGroupViewModel(src.CategoryGroup)));

            CreateMap<CategoryEditViewModel, CategoryDto>()
                .ForMember(dest => dest.CountOfUser, opts => opts.Ignore())
                .ForMember(dest => dest.CountOfEvents, opts => opts.Ignore())
                .ForMember(dest => dest.CategoryGroup, opts => opts.MapFrom(src =>
                    MapCategoryGroupDtoFromCategoryGroupViewModel(src.CategoryGroup)));

            CreateMap<UserCategory, CategoryDto>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Category.Id))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Category.Name))
              .ForMember(dest => dest.CategoryGroup, opts => opts.MapFrom(src =>
                    MapCategoryGroupDtoFromCategoryGroup(src.Category.CategoryGroup)))
              .ForAllOtherMembers(x => x.Ignore());
        }

        private static CategoryGroupDto MapCategoryGroupDtoFromCategoryGroup(CategoryGroup categoryGroup)
        {
            return categoryGroup is not null
                ? new CategoryGroupDto { Id = categoryGroup.Id, Title = categoryGroup.Title }
                : null;
        }

        private static CategoryGroupDto MapCategoryGroupDtoFromCategoryGroupViewModel(CategoryGroupViewModel categoryGroup)
        {
            return categoryGroup is not null
                ? new CategoryGroupDto { Id = categoryGroup.Id, Title = categoryGroup.Title }
                : null;
        }

        private static CategoryGroupViewModel MapCategoryGroupViewModelFromCategoryGroupDto(CategoryGroupDto categoryGroup)
        {
            return categoryGroup is not null
                ? new CategoryGroupViewModel { Id = categoryGroup.Id, Title = categoryGroup.Title }
                : null;
        }
    }
}
