using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IEventScheduleService
    {
        Task<Guid> Create(EventScheduleDTO eventScheduleDTO);

        Task<Guid> CancelEvents(Guid eventId);

        Task<Guid> CancelNextEvent(Guid eventId);

        Task<Guid> Edit(EventScheduleDTO eventScheduleDTO);

        EventScheduleDTO EventScheduleById(Guid eventScheduleId);

        EventScheduleDTO EventScheduleByEventId(Guid eventId);

        IEnumerable<EventScheduleDTO> GetAll();

        IEnumerable<EventScheduleDTO> GetUrgentEventSchedules();
    }
}
