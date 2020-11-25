using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Team : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Developer> Developers { get; set; }

        public ICollection<TeamPhoto> TeamPhotos { get; set; }
    }
}
