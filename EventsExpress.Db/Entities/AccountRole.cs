using System;

namespace EventsExpress.Db.Entities
{
    public class AccountRole
    {
        public Enums.Role RoleId { get; set; }

        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Role Role { get; set; }
    }
}
