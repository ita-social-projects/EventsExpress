using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
