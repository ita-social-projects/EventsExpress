using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using EventsExpress.Db.Repo;         
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;       
using System.Linq;
using System.Text;

namespace EventsExpress.Db.Repo
{
    public class EventRepository : Repository<Event>, IEventRepository 
    {
        public EventRepository(AppDbContext db) : base(db)
        {

        }
              
    }
}
