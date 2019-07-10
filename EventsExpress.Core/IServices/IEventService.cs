using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IEventService
    {
        Task<OperationResult> Create(EventDTO e);
        Task<OperationResult> Edit(EventDTO e);
        IEnumerable<EventDTO> Events();
        bool Exists(Guid id);
        EventDTO EventById(Guid eventId);
        bool UserIsAuthorizedToEdit(Guid eventId, Guid userId);
        EventDTO Details(Guid id);
        Task<OperationResult> Delete(Guid eventId);
        IEnumerable<EventDTO> EventsByUserId(Guid userId);
        IEnumerable<EventDTO> UpcomingThreeEvents();
        Task<OperationResult> AddUserToEvent(Guid userId, Guid eventId); 
    }
}
