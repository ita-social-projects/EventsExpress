using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IEventService
    {
        Task<OperationResult> Create(EventDTO eventDTO);

        Task<OperationResult> Edit(EventDTO e);

        Task<OperationResult> Delete(Guid eventId);

        Task<OperationResult> BlockEvent(Guid eID);

        Task<OperationResult> UnblockEvent(Guid eId);

        EventDTO EventById(Guid eventId);

        IEnumerable<EventDTO> GetAll(EventFilterViewModel model, out int count);

        IEnumerable<EventDTO> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDTO> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDTO> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModelq);

        IEnumerable<EventDTO> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel);

        Task<OperationResult> AddUserToEvent(Guid userId, Guid eventId);

        Task<OperationResult> DeleteUserFromEvent(Guid userId, Guid eventId);

        Task<OperationResult> SetRate(Guid userId, Guid eventId, byte rate);

        Task<OperationResult> ApproveUserToEvent(Guid userId, Guid eventId, bool action);

        byte GetRateFromUser(Guid userId, Guid eventId);

        double GetRate(Guid eventId);

        bool UserIsVisitor(Guid userId, Guid eventId);

        bool Exists(Guid eventId);
    }
}
