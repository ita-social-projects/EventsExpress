using System;
using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class Account : BaseEntity
    {
        public Guid? UserId { get; set; }

        public bool IsBlocked { get; set; }

        public User User { get; set; }

        public AuthLocal AuthLocal { get; set; }

        public IEnumerable<AuthExternal> AuthExternal { get; set; }

        public IEnumerable<AccountRole> AccountRoles { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
