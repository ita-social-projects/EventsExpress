using System;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.IRepo
{
    public interface IEventStatusHistoryRepository : IRepository<EventStatusHistory>
    {
        EventStatusHistory GetLastRecord(Guid eventId, EventStatus status);
    }
}
