using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Azure.Storage.Blobs;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ValueResolvers;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using NetTopologySuite.Geometries;

namespace EventsExpress.Mapping
{
    public class EventMapperProfile : Profile
    {
        public EventMapperProfile()
        {
            CreateMap<Event, EventDto>()
               .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
               .ForMember(dest => dest.Owners, opt => opt.MapFrom(x => x.Owners.Select(z => z.User)))
               .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => MapCategoryToCategoryDto(x))))
               .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.EventSchedule.Frequency))
               .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.EventSchedule.Periodicity))
               .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => (src.EventSchedule != null)))
               .ForMember(dest => dest.EventStatus, opts => opts.MapFrom(src => src.StatusHistory.LastOrDefault().EventStatus))
               .ForMember(dest => dest.Inventories, opt => opt.MapFrom(src =>
                    src.Inventories.Select(x => MapInventoryDtoFromInventory(x))))
               .ForMember(dest => dest.OwnerIds, opts => opts.Ignore())
               .ForMember(dest => dest.Photo, opts => opts.Ignore());

            CreateMap<EventDto, Event>()
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.Owners.Select(x =>
                   new EventOwner
                   {
                       UserId = x.Id,
                       EventId = src.Id,
                   })))
                .ForMember(dest => dest.Visitors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                    src.Inventories.Select(x => MapInventoryFromInventoryDto(x))))
                .ForMember(dest => dest.EventLocationId, opts => opts.Ignore())
                .ForMember(dest => dest.EventLocation, opts => opts.Ignore())
                .ForMember(dest => dest.EventSchedule, opts => opts.Ignore())
                .ForMember(dest => dest.Rates, opts => opts.Ignore())
                .ForMember(dest => dest.StatusHistory, opts => opts.Ignore());

            CreateMap<EventDto, EventPreviewViewModel>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => MapCategoryViewModelFromCategoryDto(x))))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
                .ForMember(dest => dest.CountVisitor, opts => opts.MapFrom(src => src.Visitors.Count(x => x.UserStatusEvent == 0)))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.EventStatus, opts => opts.MapFrom(src => src.EventStatus))
                .ForMember(dest => dest.Owners, opts => opts.MapFrom(src => src.Owners.Select(x => MapUserToUserPreviewViewModel(x))))
                .ForMember(dest => dest.Members, opts => opts.MapFrom<EventDtoToVisitorsResolver>());

            CreateMap<EventDto, EventViewModel>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => MapCategoryViewModelFromCategoryDto(x))))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                    src.Inventories.Select(x => MapInventoryViewModelFromInventoryDto(x))))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
                .ForMember(dest => dest.Visitors, opts => opts.MapFrom<EventDtoToVisitorsResolver>())
                .ForMember(dest => dest.Owners, opts => opts.MapFrom<EventDtoToOwnersResolver>())
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.Members, opts => opts.Ignore());

            CreateMap<EventEditViewModel, EventDto>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => MapCategoryViewModelToCategoryDto(x))))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                    src.Inventories.Select(x => MapInventoryDtoFromInventoryViewModel(x))))
                .ForMember(dest => dest.Owners, opts => opts.Ignore())
                .ForMember(dest => dest.OwnerIds, opts => opts.MapFrom(src => src.Owners.Select(x => x.Id)))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
                .ForMember(dest => dest.Photo, opts => opts.Ignore())
                .ForMember(dest => dest.Visitors, opts => opts.Ignore());

            CreateMap<EventCreateViewModel, EventDto>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => MapCategoryViewModelToCategoryDto(x))))
                .ForMember(dest => dest.Owners, opts => opts.Ignore())
                .ForMember(dest => dest.OwnerIds, opts => opts.MapFrom(src => src.Owners.Select(x => x.Id)))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                    src.Inventories.Select(x => MapInventoryDtoFromInventoryViewModel(x))))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Visitors, opts => opts.Ignore());
        }

        private static LocationDto MapLocation(Event e)
        {
            if (e.EventLocation != null)
            {
                return new LocationDto()
                {
                    Id = e.Id,
                    OnlineMeeting = e.EventLocation.OnlineMeeting,
                    Point = e.EventLocation.Point,
                    Type = e.EventLocation.Type,
                };
            }

            return null;
        }

        private static LocationDto MapLocation(EventEditViewModel e)
        {
            LocationDto locationDto = null;

            if (e.Location != null)
            {
                locationDto = new LocationDto();
                if (e.Location.Type != null)
                {
                    locationDto.Type = e.Location.Type;
                }

                if (e.Location.OnlineMeeting != null && e.Location.Type == LocationType.Online)
                {
                    locationDto.OnlineMeeting = e.Location.OnlineMeeting;
                }

                if (e.Location.Type == LocationType.Map)
                {
                    locationDto.Point = PointOrNullEdit(e);
                }
            }

            return locationDto;
        }

        private static LocationDto MapLocation(EventCreateViewModel e)
        {
            LocationDto locationDto = null;

            if (e.Location != null)
            {
                locationDto = new LocationDto();
                if (e.Location.Type != null)
                {
                    locationDto.Type = e.Location.Type;
                }

                if (e.Location.OnlineMeeting != null && e.Location.Type == LocationType.Online)
                {
                    locationDto.OnlineMeeting = e.Location.OnlineMeeting;
                }

                if (e.Location.Type == LocationType.Map)
                {
                    locationDto.Point = PointOrNullCreate(e);
                }
            }

            return locationDto;
        }

        private static LocationViewModel MapLocation(EventDto eventDto)
        {
            if (eventDto.Location == null)
            {
                return null;
            }

            return eventDto.Location.Type == LocationType.Map ?
              new LocationViewModel
              {
                  Latitude = eventDto.Location.Point.X,
                  Longitude = eventDto.Location.Point.Y,
                  OnlineMeeting = null,
                  Type = eventDto.Location.Type,
              }
                      :
                    new LocationViewModel
                    {
                        Latitude = null,
                        Longitude = null,
                        OnlineMeeting = eventDto.Location.OnlineMeeting,
                        Type = eventDto.Location.Type,
                    };
        }

        private static Point PointOrNullEdit(EventEditViewModel editViewModel)
        {
            return editViewModel.Location?.Type == LocationType.Map ?
                 new Point(editViewModel.Location.Latitude.Value, editViewModel.Location.Longitude.Value) { SRID = 4326 } : null;
        }

        private static Point PointOrNullCreate(EventCreateViewModel createViewModel)
        {
            return createViewModel.Location.Type == LocationType.Map ?
                 new Point(createViewModel.Location.Latitude.Value, createViewModel.Location.Longitude.Value) { SRID = 4326 } : null;
        }

        private static string UserName(User user)
        {
            return user.Name ?? user.Email.Substring(0, user.Email.IndexOf("@", StringComparison.Ordinal));
        }

        private static CategoryDto MapCategoryToCategoryDto(EventCategory eventCategory)
        {
            return new CategoryDto { Id = eventCategory.Category.Id, Name = eventCategory.Category.Name };
        }

        private static CategoryDto MapCategoryViewModelToCategoryDto(CategoryViewModel categoryViewModel)
        {
            return new CategoryDto
            {
                    Id = categoryViewModel.Id,
                    Name = categoryViewModel.Name,
            };
        }

        private static CategoryViewModel MapCategoryViewModelFromCategoryDto(CategoryDto categoryDto)
        {
            return new CategoryViewModel
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
            };
        }

        private static UnitOfMeasuringDto MapInventoryToUnitOfMeasuringDto(Inventory inventory)
        {
            return new UnitOfMeasuringDto
            {
                Id = inventory.UnitOfMeasuring.Id,
                UnitName = inventory.UnitOfMeasuring.UnitName,
                ShortName = inventory.UnitOfMeasuring.ShortName,
            };
        }

        private static UnitOfMeasuringViewModel MapUnitOfMeasuringViewModelFromInventoryDto(InventoryDto inventoryDto)
        {
            return new UnitOfMeasuringViewModel
            {
                Id = inventoryDto.UnitOfMeasuring.Id,
                UnitName = inventoryDto.UnitOfMeasuring.UnitName,
                ShortName = inventoryDto.UnitOfMeasuring.ShortName,
            };
        }

        private static UnitOfMeasuringDto MapUnitOfMeasuringDtoFromInventoryViewModel(InventoryViewModel inventoryViewModel)
        {
            return new UnitOfMeasuringDto
            {
                Id = inventoryViewModel.UnitOfMeasuring.Id,
                UnitName = inventoryViewModel.UnitOfMeasuring.UnitName,
                ShortName = inventoryViewModel.UnitOfMeasuring.ShortName,
            };
        }

        private static InventoryDto MapInventoryDtoFromInventoryViewModel(InventoryViewModel inventoryViewModel)
        {
            return new InventoryDto
            {
                Id = inventoryViewModel.Id,
                ItemName = inventoryViewModel.ItemName,
                NeedQuantity = inventoryViewModel.NeedQuantity,
                UnitOfMeasuring = MapUnitOfMeasuringDtoFromInventoryViewModel(inventoryViewModel),
            };
        }

        private static InventoryViewModel MapInventoryViewModelFromInventoryDto(InventoryDto inventoryDto)
        {
            return new InventoryViewModel
            {
                Id = inventoryDto.Id,
                ItemName = inventoryDto.ItemName,
                NeedQuantity = inventoryDto.NeedQuantity,
                UnitOfMeasuring = MapUnitOfMeasuringViewModelFromInventoryDto(inventoryDto),
            };
        }

        private static Inventory MapInventoryFromInventoryDto(InventoryDto inventoryDto)
        {
            return new Inventory
            {
                Id = inventoryDto.Id,
                ItemName = inventoryDto.ItemName,
                NeedQuantity = inventoryDto.NeedQuantity,
                UnitOfMeasuringId = inventoryDto.UnitOfMeasuring.Id,
            };
        }

        private static InventoryDto MapInventoryDtoFromInventory(Inventory inventory)
        {
            return new InventoryDto
            {
                Id = inventory.Id,
                ItemName = inventory.ItemName,
                NeedQuantity = inventory.NeedQuantity,
                UnitOfMeasuring = MapInventoryToUnitOfMeasuringDto(inventory),
            };
        }

        private UserPreviewViewModel MapUserToUserPreviewViewModel(User user)
        {
            return new UserPreviewViewModel
            {
                Birthday = user.Birthday,
                Id = user.Id,
                Username = UserName(user),
            };
        }
    }
}
