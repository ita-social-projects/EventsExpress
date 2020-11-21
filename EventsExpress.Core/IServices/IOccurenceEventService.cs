using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IOccurenceEventService
    {
        Task<OperationResult> Create(OccurenceEventDTO eventDTO);

        Task<OperationResult> CancelEvents(Guid eventId);

        Task<OperationResult> CancelNextEvent(Guid eventId);

        Task<OperationResult> Edit(OccurenceEventDTO eventDTO);

        OccurenceEventDTO OccurenceEventById(Guid eventId);

        OccurenceEventDTO OccurenceEventByEventId(Guid eventId);

        IEnumerable<OccurenceEventDTO> GetAll();

        IEnumerable<OccurenceEventDTO> GetUrgentOccurenceEvents();
    }
}
