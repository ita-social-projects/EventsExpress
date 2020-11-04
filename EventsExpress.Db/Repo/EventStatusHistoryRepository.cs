using System;
using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class EventStatusHistoryRepository : Repository<EventStatusHistory>, IEventStatusHistoryRepository
    {
        public EventStatusHistoryRepository(AppDbContext db)
            : base(db)
        {
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return Database.EventStatusHistory.Where(e => e.EventId == eventId && e.EventStatus == status).Last();
        }
    }
}
