using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IEventService
    {
        Task<Guid> Create(EventDTO eventDTO);

        Task<Guid> CreateNextEvent(Guid eventId);

        Task<Guid> EditNextEvent(EventDTO eventDTO);

        Task<Guid> Edit(EventDTO e);

        Task Delete(Guid eventId);

        Task BlockEvent(Guid eID);

        Task UnblockEvent(Guid eId);

        EventDTO EventById(Guid eventId);

        IEnumerable<EventDTO> GetAll(EventFilterViewModel model, out int count);

        IEnumerable<EventDTO> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDTO> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDTO> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModelq);

        IEnumerable<EventDTO> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel);

        Task AddUserToEvent(Guid userId, Guid eventId);

        Task DeleteUserFromEvent(Guid userId, Guid eventId);

        Task SetRate(Guid userId, Guid eventId, byte rate);

        Task ChangeVisitorStatus(Guid userId, Guid eventId, UserStatusEvent status);

        byte GetRateFromUser(Guid userId, Guid eventId);

        double GetRate(Guid eventId);

        bool UserIsVisitor(Guid userId, Guid eventId);

        bool Exists(Guid eventId);
    }
}
