using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
