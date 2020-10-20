using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
