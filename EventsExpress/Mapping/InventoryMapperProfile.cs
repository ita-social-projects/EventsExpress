using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class InventoryMapperProfile : Profile
    {
        public InventoryMapperProfile()
        {
            CreateMap<Inventory, InventoryDTO>()
                .ForMember(dest => dest.UnitOfMeasuring, opt => opt.MapFrom(src => new UnitOfMeasuringDTO 
                {
                    Id = src.UnitOfMeasuring.Id,
                    ShortName = src.UnitOfMeasuring.ShortName,
                    UnitName = src.UnitOfMeasuring.UnitName,
                }));

            CreateMap<InventoryDTO, Inventory>()
                .ForMember(dest => dest.UnitOfMeasuring, opt => opt.Ignore())
                .ForMember(dest => dest.Event, opts => opts.Ignore())
                .ForMember(dest => dest.EventId, opts => opts.Ignore())
                .ForMember(dest => dest.UserEventInventories, opts => opts.Ignore());

            CreateMap<InventoryDTO, InventoryViewModel>()
                 .ForMember(dest => dest.UnitOfMeasuring, opt => opt.MapFrom(src => new UnitOfMeasuringViewModel
                 {
                     Id = src.UnitOfMeasuring.Id,
                     ShortName = src.UnitOfMeasuring.ShortName,
                     UnitName = src.UnitOfMeasuring.UnitName,
                 }));

            CreateMap<InventoryViewModel, InventoryDTO>()
                 .ForMember(dest => dest.UnitOfMeasuring, opt => opt.MapFrom(src => new UnitOfMeasuringDTO
                 {
                     Id = src.UnitOfMeasuring.Id,
                     ShortName = src.UnitOfMeasuring.ShortName,
                     UnitName = src.UnitOfMeasuring.UnitName,
                 }));
        }
    }
}
