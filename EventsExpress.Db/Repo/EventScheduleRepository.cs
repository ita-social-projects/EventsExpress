using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class EventScheduleRepository : Repository<EventSchedule>, IEventScheduleRepository
    {
        public EventScheduleRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
