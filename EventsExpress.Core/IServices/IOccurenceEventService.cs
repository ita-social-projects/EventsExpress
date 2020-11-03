using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IOccurenceEventService
    {
        Task<OperationResult> Create(OccurenceEventDTO eventDTO);

        Task<OperationResult> Edit(OccurenceEventDTO e);

        Task<OperationResult> Delete(Guid eventId);

        OccurenceEventDTO OccurenceEventById(Guid eventId);

        IEnumerable<OccurenceEventDTO> Events(EventFilterViewModel model, out int count);

        bool Exists(Guid eventId);
    }
}
