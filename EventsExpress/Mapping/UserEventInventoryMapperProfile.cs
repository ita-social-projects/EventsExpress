using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class UserEventInventoryMapperProfile : Profile
    {
        public UserEventInventoryMapperProfile()
        {
            CreateMap<UserEventInventory, UserEventInventoryDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserDto
                {
                    Id = src.UserId,
                    FirstName = src.UserEvent.User.FirstName,
                }));
            CreateMap<UserEventInventoryDto, UserEventInventoryViewModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserPreviewViewModel
                {
                    Id = src.User.Id,
                    FirstName = src.User.FirstName,
                }));
            CreateMap<UserEventInventoryViewModel, UserEventInventoryDto>()
                .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<UserEventInventoryDto, UserEventInventory>()
                .ForMember(dest => dest.UserEvent, opt => opt.Ignore())
                .ForMember(dest => dest.Inventory, opt => opt.Ignore());
        }
    }
}
