using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
