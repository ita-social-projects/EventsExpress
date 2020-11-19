using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;

namespace EventsExpress.Mapping
{
    public class OccurenceEventAutoMapper : Profile
    {
        public OccurenceEventAutoMapper()
        {
            CreateMap<OccurenceEvent, OccurenceEventDTO>()
           .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
           .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
           .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
           .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.LastRun))
           .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => src.NextRun))
           .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
           .ForMember(dest => dest.Event, opts => opts.MapFrom(src => new EventDTO
           {
               Id = src.Event.Id,
               Title = src.Event.Title,
               DateTo = src.Event.DateTo,
               DateFrom = src.Event.DateFrom,
               OwnerId = src.Event.OwnerId,
               PhotoBytes = src.Event.Photo,
           }));

            CreateMap<OccurenceEventDTO, OccurenceEvent>().ReverseMap();

            CreateMap<OccurenceEventDTO, OccurenceEventDto>()
                .ForMember(dest => dest.Event, opts => opts.MapFrom(src => new EventDTO
                {
                    Id = src.Event.Id,
                    Title = src.Event.Title,
                    DateTo = src.Event.DateTo,
                    DateFrom = src.Event.DateFrom,
                    OwnerId = src.Event.OwnerId,
                    PhotoUrl = src.Event.PhotoBytes.Img.ToRenderablePictureString(),
                }));

            CreateMap<OccurenceEventDto, OccurenceEventDTO>()
                .ForMember(dest => dest.Event, opts => opts.MapFrom(src => new EventDTO
                {
                    Id = src.Event.Id,
                    Title = src.Event.Title,
                    DateTo = src.Event.DateTo,
                    DateFrom = src.Event.DateFrom,
                    OwnerId = src.Event.OwnerId,
                    PhotoUrl = src.Event.PhotoBytes.Img.ToRenderablePictureString(),
                }));

            CreateMap<EventDTO, OccurenceEventDTO>()
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => DateTimeExtensions.AddDateUnit(src.Periodicity, src.Frequency, src.DateTo)))
                .ForMember(dest => dest.CreatedBy, opts => opts.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModifiedBy, opts => opts.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.ModifiedDate, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => true));
        }
    }
}
