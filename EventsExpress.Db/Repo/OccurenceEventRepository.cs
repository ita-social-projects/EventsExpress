using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class OccurenceEventRepository : Repository<OccurenceEvent>, IOccurenceEventRepository
    {
        public OccurenceEventRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
