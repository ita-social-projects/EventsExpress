using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class UnitOfMeasuringRepository : Repository<UnitOfMeasuring>, IUnitOfMeasuringRepository
    {
        public UnitOfMeasuringRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
