using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
        //navigation properties:
    }
}
