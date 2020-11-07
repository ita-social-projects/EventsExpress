using System;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IEventStatusHistoryService
    {
        Task<OperationResult> CancelEvent(Guid eventId, string reason);

        EventStatusHistory GetLastRecord(Guid eventId, EventStatus status);
    }
}
