using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IEventScheduleService
    {
        Task<OperationResult> Create(EventScheduleDTO eventScheduleDTO);

        Task<OperationResult> CancelEvents(Guid eventId);

        Task<OperationResult> CancelNextEvent(Guid eventId);

        Task<OperationResult> Edit(EventScheduleDTO eventScheduleDTO);

        EventScheduleDTO EventScheduleById(Guid eventScheduleId);

        EventScheduleDTO EventScheduleByEventId(Guid eventId);

        IEnumerable<EventScheduleDTO> GetAll();

        IEnumerable<EventScheduleDTO> GetUrgentEventSchedules();
    }
}
