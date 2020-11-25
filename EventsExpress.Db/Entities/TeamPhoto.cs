using System;

namespace EventsExpress.Db.Entities
{
    public class TeamPhoto
    {
        public Guid TeamId { get; set; }

        public Team Team { get; set; }

        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; }
    }
}
