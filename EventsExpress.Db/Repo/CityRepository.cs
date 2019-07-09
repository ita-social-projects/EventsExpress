using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsExpress.Db.Repo
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AppDbContext db) : base(db)
        {

        }
       
    }
}
