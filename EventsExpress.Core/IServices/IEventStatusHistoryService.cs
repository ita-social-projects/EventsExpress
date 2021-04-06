using System;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IEventStatusHistoryService
    {
        Task SetStatusEvent(Guid eventId, string reason, EventStatus eventStatus);

        EventStatusHistory GetLastRecord(Guid eventId, EventStatus status);
    }
}
