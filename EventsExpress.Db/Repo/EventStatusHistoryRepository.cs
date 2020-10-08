using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class EventStatusHistoryRepository : Repository<EventStatusHistory>, IEventStatusHistoryRepository
    {
        public EventStatusHistoryRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
