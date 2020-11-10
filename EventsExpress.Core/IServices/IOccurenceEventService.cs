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

        Task<OperationResult> Delete(Guid eventId);

        Task<OperationResult> Edit(OccurenceEventDTO eventDTO);

        OccurenceEventDTO EventById(Guid eventId);

        Task EventNotification(CancellationToken stoppingToken);

        IEnumerable<OccurenceEventDTO> GetAll();
    }
}
