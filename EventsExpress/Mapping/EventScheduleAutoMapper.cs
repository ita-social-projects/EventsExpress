using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class EventScheduleAutoMapper : Profile
    {
        public EventScheduleAutoMapper()
        {
            CreateMap<EventSchedule, EventScheduleDTO>()
           .ForMember(dest => dest.Event, opts => opts.MapFrom(src => new EventDTO
           {
               Id = src.Event.Id,
               Title = src.Event.Title,
               DateTo = src.Event.DateTo,
               DateFrom = src.Event.DateFrom,
               Owners = src.Event.Owners.Select(x => new User
               {
                   Id = x.UserId,
               }),
               PhotoBytes = src.Event.Photo,
           }));

            CreateMap<EventScheduleDTO, EventSchedule>().ReverseMap();

            CreateMap<EventScheduleDTO, EventScheduleViewModel>()
                .ForMember(dest => dest.Event, opts => opts.MapFrom(src => new EventPreviewViewModel
                {
                    Id = src.Event.Id,
                    Title = src.Event.Title,
                    DateTo = src.Event.DateTo,
                    DateFrom = src.Event.DateFrom,
                    Owners = src.Event.Owners.Select(x => new UserPreviewViewModel
                    {
                        Id = x.Id,
                    }),
                    PhotoUrl = src.Event.PhotoBytes.Img.ToRenderablePictureString(),
                }));

            CreateMap<EventScheduleViewModel, EventScheduleDTO>()
                .ForMember(dest => dest.Event, opts => opts.Ignore());

            CreateMap<EventDTO, EventScheduleDTO>()
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => DateTimeExtensions.AddDateUnit(src.Periodicity, src.Frequency, src.DateTo)))
                .ForMember(dest => dest.CreatedDateTime, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModifiedDateTime, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => true));
        }
    }
}
