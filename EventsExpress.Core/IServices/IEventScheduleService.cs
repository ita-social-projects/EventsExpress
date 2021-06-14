using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IEventScheduleService
    {
        Task<Guid> Create(EventScheduleDto eventScheduleDTO);

        Task<Guid> CancelEvents(Guid eventId);

        Task<Guid> Delete(Guid id);

        Task<Guid> CancelNextEvent(Guid eventId);

        Task<Guid> Edit(EventScheduleDto eventScheduleDTO);

        EventScheduleDto EventScheduleById(Guid eventScheduleId);

        EventScheduleDto EventScheduleByEventId(Guid eventId);

        IEnumerable<EventScheduleDto> GetAll();

        IEnumerable<EventScheduleDto> GetUrgentEventSchedules();
    }
}
