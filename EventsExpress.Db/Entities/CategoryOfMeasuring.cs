using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    public class CategoryOfMeasuring : BaseEntity
    {
        public string CategoryName { get; set; }

        public ICollection<UnitOfMeasuring> UnitOfMeasurings { get; set; }
    }
}
