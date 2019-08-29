using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Rate : BaseEntity
    {
        public Guid UserFromId { get; set; }      
        public virtual User UserFrom { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; } 

        public byte Score { get; set; }
    }
}
