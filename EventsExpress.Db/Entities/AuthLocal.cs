using System;

namespace EventsExpress.Db.Entities
{
    public class AuthLocal : AuthBase
    {
        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
