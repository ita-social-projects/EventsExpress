using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class RateRepository : Repository<Rate>, IRateRepository
    {
        public RateRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
