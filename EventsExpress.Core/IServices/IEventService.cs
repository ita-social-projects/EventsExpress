using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IEventService
    {
        Task<Guid> Create(EventDto eventDTO);

        Task<Guid> CreateNextEvent(Guid eventId);

        Task<Guid> EditNextEvent(EventDto eventDTO);

        Task<Guid> Edit(EventDto e);

        Task BlockEvent(Guid eventId);

        Task UnblockEvent(Guid eventId);

        EventDto EventById(Guid eventId);

        IEnumerable<EventDto> GetAll(EventFilterViewModel model, out int count);

        IEnumerable<EventDto> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDto> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDto> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDto> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModel);

        IEnumerable<EventDto> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel);

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
