using System;

namespace EventsExpress.Db.Entities
{
    public abstract class AuthBase : BaseEntity
    {
        public string Email { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}
