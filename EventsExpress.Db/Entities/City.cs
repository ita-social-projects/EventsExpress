using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }

    }
}
