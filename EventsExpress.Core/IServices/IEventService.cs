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

        Task<OperationResult> Create(EventDTO eventDTO);
        Task<OperationResult> Edit(EventDTO e);
        Task<OperationResult> Delete(Guid eventId);

        EventDTO EventById(Guid eventId);       

        IEnumerable<EventDTO>  Events(EventFilterViewModel model, out int count);
        
        IEnumerable<EventDTO> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);
        IEnumerable<EventDTO> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);
        IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);
        IEnumerable<EventDTO> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModelq);

        Task<OperationResult> AddUserToEvent(Guid userId, Guid eventId);
        Task<OperationResult> DeleteUserFromEvent(Guid userId, Guid eventId);
        
    }
}
