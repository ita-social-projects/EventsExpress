using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            CreateMap<UserEventInventory, UserEventInventoryDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserDTO
                {
                    Id = src.UserId,
                    Name = src.UserEvent.User.Name,
                }));
            CreateMap<UserEventInventoryDTO, UserEventInventoryViewModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserPreviewViewModel
                {
                    Id = src.User.Id,
                    Username = src.User.Name,
                }));
            CreateMap<UserEventInventoryViewModel, UserEventInventoryDTO>()
                .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<UserEventInventoryDTO, UserEventInventory>()
                .ForMember(dest => dest.UserEvent, opt => opt.Ignore())
                .ForMember(dest => dest.Inventory, opt => opt.Ignore());
        }
    }
}
