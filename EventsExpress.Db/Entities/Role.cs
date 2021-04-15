using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class Role
    {
        public Enums.Role Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AccountRole> Accounts { get; set; }
    }
}
