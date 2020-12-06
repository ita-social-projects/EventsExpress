using System;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IEventStatusHistoryService
    {
        Task CancelEvent(Guid eventId, string reason);

        EventStatusHistory GetLastRecord(Guid eventId, EventStatus status);
    }
}
