using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    using Event = EventsExpress.Db.Entities.Event;

    internal static class EventTestHelpers
    {
        public static Event MapEventFromEventDto(EventDto eventDto)
        {
            if (eventDto is null)
            {
                return null;
            }

            return new Event
            {
                Id = eventDto.Id,
                Title = eventDto.Title,
                Description = eventDto.Description,
                DateFrom = eventDto.DateFrom,
                DateTo = eventDto.DateTo,
                MaxParticipants = eventDto.MaxParticipants,
            };
        }

        public static EventDto MapEventDtoFromEvent(Event ev)
        {
            if (ev is null)
            {
                return null;
            }

            return new EventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                DateFrom = ev.DateFrom,
                DateTo = ev.DateTo,
                MaxParticipants = ev.MaxParticipants,
            };
        }

        public static EventScheduleDto MapEventScheduleDtoFromEventDto(EventDto eventDto)
        {
            if (eventDto is null)
            {
                return null;
            }

            return new EventScheduleDto
            {
                Id = eventDto.Id,
                IsActive = true,
                Frequency = eventDto.Frequency,
                Periodicity = eventDto.Periodicity,
                LastRun = DateTime.Today,
                NextRun = DateTime.Today.AddDays(7),
                Event = eventDto,
                EventId = eventDto.Id,
            };
        }

        public static EventScheduleDto GetEventSchedule()
        {
            return new EventScheduleDto
            {
                IsActive = true,
                Frequency = 1,
                Periodicity = Periodicity.Weekly,
                NextRun = DateTime.Today.AddDays(7),
            };
        }

        public static EventDto DeepCopyDto(EventDto eventDto)
        {
            if (eventDto is null)
            {
                return null;
            }

            return new EventDto
            {
                Id = eventDto.Id,
                DateFrom = eventDto.DateFrom,
                DateTo = eventDto.DateTo,
                Description = eventDto.Description,
                Organizers = new List<User>(eventDto.Organizers),
                Title = eventDto.Title,
                IsPublic = eventDto.IsPublic,
                Categories = eventDto.Categories,
                MaxParticipants = eventDto.MaxParticipants,
                Location = new LocationDto
                {
                    Point = eventDto.Location.Point,
                    Type = eventDto.Location.Type,
                },
            };
        }
    }
}
