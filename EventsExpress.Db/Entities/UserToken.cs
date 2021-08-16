using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class UserToken : BaseEntity
    {
        public TokenType Type { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }

        public string ReplacedByToken { get; set; }

        public string CreatedByIp { get; set; }

        public string RevokedByIp { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}
