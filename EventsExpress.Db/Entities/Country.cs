using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<City> Cities { get; set; }
    }
}
