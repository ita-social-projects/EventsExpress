using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class EventOwner
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
