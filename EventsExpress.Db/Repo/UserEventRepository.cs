using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;

namespace EventsExpress.Db.Repo
{
    public class UserEventRepository : Repository<UserEvent>, IUserEventRepository
    {
        public UserEventRepository(AppDbContext db)
            : base(db)
        {
        }

        public override UserEvent Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
