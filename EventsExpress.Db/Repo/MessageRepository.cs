using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Repo
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
