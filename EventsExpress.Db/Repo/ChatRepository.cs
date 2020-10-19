using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class ChatRepository : Repository<ChatRoom>, IChatRepository
    {
        public ChatRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
