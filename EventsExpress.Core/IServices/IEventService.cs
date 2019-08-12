using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IEventService
    {
        bool Exists(Guid id);

        Task<OperationResult> Create(EventDTO e);
        Task<OperationResult> Edit(EventDTO e);
        Task<OperationResult> Delete(Guid eventId);

        EventDTO EventById(Guid eventId);
        EventDTO Details(Guid id);

        IEnumerable<EventDTO>  Events(EventFilterViewModel model, out int Count);
        IEnumerable<EventDTO> EventsByUserId(Guid userId);
        IEnumerable<EventDTO> UpcomingEvents(int? num);

        IEnumerable<EventDTO> FutureEventsByUserId(Guid userId);
        IEnumerable<EventDTO> PastEventsByUserId(Guid userId);
        IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId);
        IEnumerable<EventDTO> EventsToGoByUserId(Guid userId);

        Task<OperationResult> AddUserToEvent(Guid userId, Guid eventId);
        Task<OperationResult> DeleteUserFromEvent(Guid userId, Guid eventId);
        
    }
}
