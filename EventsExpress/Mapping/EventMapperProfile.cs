using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;
using NetTopologySuite.Geometries;

namespace EventsExpress.Mapping
{
    public class EventMapperProfile : Profile
    {
        public EventMapperProfile()
        {
            CreateMap<Event, EventDto>()
               .ForMember(dest => dest.Photo, opt => opt.Ignore())
               .ForMember(dest => dest.Point, opts => opts.MapFrom(src => src.EventLocation.Point))
               .ForMember(dest => dest.Owners, opt => opt.MapFrom(x => x.Owners.Select(z => z.User)))
               .ForMember(
                   dest => dest.Categories,
                   opts => opts.MapFrom(src =>
                       src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })))
               .ForMember(dest => dest.PhotoBytes, opt => opt.MapFrom(src => src.Photo))
               .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.EventSchedule.Frequency))
               .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.EventSchedule.Periodicity))
               .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => (src.EventSchedule != null)))
               .ForMember(dest => dest.PhotoId, opts => opts.MapFrom(src => src.PhotoId))
               .ForMember(dest => dest.EventStatus, opts => opts.MapFrom(src => src.StatusHistory.LastOrDefault().EventStatus))
               .ForMember(dest => dest.Inventories, opt => opt.MapFrom(src =>
                       src.Inventories.Select(x => new InventoryDto
                       {
                           Id = x.Id,
                           ItemName = x.ItemName,
                           NeedQuantity = x.NeedQuantity,
                           UnitOfMeasuring = new UnitOfMeasuringDto
                           {
                               Id = x.UnitOfMeasuring.Id,
                               UnitName = x.UnitOfMeasuring.UnitName,
                               ShortName = x.UnitOfMeasuring.ShortName,
                           },
                       })))
               .ForMember(dest => dest.PhotoUrl, opts => opts.Ignore())
               .ForMember(dest => dest.OwnerIds, opts => opts.Ignore());

            CreateMap<EventDto, Event>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.Owners.Select(x =>
                   new EventOwner
                   {
                       UserId = x.Id,
                       EventId = src.Id,
                   })))
                .ForMember(dest => dest.Visitors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new Inventory
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuringId = x.UnitOfMeasuring.Id,
                        })))
                .ForMember(dest => dest.EventLocationId, opts => opts.Ignore())
                .ForMember(dest => dest.EventLocation, opts => opts.Ignore())
                .ForMember(dest => dest.EventSchedule, opts => opts.Ignore())
                .ForMember(dest => dest.Rates, opts => opts.Ignore())
                .ForMember(dest => dest.StatusHistory, opts => opts.Ignore());

            CreateMap<EventDto, EventPreviewViewModel>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoBytes.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Point.X))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Point.Y))
                .ForMember(dest => dest.CountVisitor, opts => opts.MapFrom(src => src.Visitors.Count(x => x.UserStatusEvent == 0)))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.EventStatus, opts => opts.MapFrom(src => src.EventStatus))
                .ForMember(dest => dest.Owners, opts => opts.MapFrom(src => src.Owners.Select(x =>
                   new UserPreviewViewModel
                   {
                       Birthday = x.Birthday,
                       Id = x.Id,
                       PhotoUrl = x.Photo != null ? x.Photo.Thumb.ToRenderablePictureString() : null,
                       Username = x.Name ?? x.Email.Substring(0, x.Email.IndexOf("@", StringComparison.Ordinal)),
                   })));

            CreateMap<EventDto, EventViewModel>()
                 .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoBytes.Img.ToRenderablePictureString()))
                 .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => new CategoryViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                 })))
                 .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryViewModel
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringViewModel
                            {
                                Id = x.UnitOfMeasuring.Id,
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Point.X))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Point.Y))
                .ForMember(dest => dest.Visitors, opts => opts.MapFrom(src => src.Visitors.Select(x =>
                    new UserPreviewViewModel
                    {
                        Id = x.User.Id,
                        Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@", StringComparison.Ordinal)),
                        Birthday = x.User.Birthday,
                        PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                        UserStatusEvent = x.UserStatusEvent,
                    })))
                .ForMember(dest => dest.Owners, opts => opts.MapFrom(src => src.Owners.Select(x =>
                 new UserPreviewViewModel
                 {
                     Id = x.Id,
                     Username = x.Name ?? x.Email.Substring(0, x.Email.IndexOf("@", StringComparison.Ordinal)),
                     Birthday = x.Birthday,
                     PhotoUrl = x.Photo != null ? x.Photo.Thumb.ToRenderablePictureString() : null,
                 })))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants));

            CreateMap<EventEditViewModel, EventDto>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryDto
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringDto
                            {
                                Id = x.UnitOfMeasuring.Id,
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })))
                .ForMember(dest => dest.Owners, opts => opts.Ignore())
                .ForMember(dest => dest.OwnerIds, opts => opts.MapFrom(src => src.Owners.Select(x => x.Id)))
                .ForMember(dest => dest.Point, opts => opts.MapFrom(src => new Point(src.Latitude, src.Longitude) { SRID = 4326 }))
                .ForMember(dest => dest.IsBlocked, opts => opts.Ignore())
                .ForMember(dest => dest.PhotoBytes, opts => opts.Ignore())
                .ForMember(dest => dest.Visitors, opts => opts.Ignore());

            CreateMap<EventCreateViewModel, EventDto>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })))
                .ForMember(dest => dest.Owners, opts => opts.Ignore())
                .ForMember(dest => dest.OwnerIds, opts => opts.MapFrom(src => src.Owners.Select(x => x.Id)))
                .ForMember(dest => dest.Point, opts => opts.MapFrom(src => new Point(src.Latitude, src.Longitude) { SRID = 4326 }))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryDto
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringDto
                            {
                                Id = x.UnitOfMeasuring.Id,
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.IsBlocked, opts => opts.Ignore())
                .ForMember(dest => dest.PhotoUrl, opts => opts.Ignore())
                .ForMember(dest => dest.PhotoBytes, opts => opts.Ignore())
                .ForMember(dest => dest.Visitors, opts => opts.Ignore());
        }
    }
}
