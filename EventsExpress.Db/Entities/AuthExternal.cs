using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class AuthExternal : AuthBase
    {
        public AuthExternalType Type { get; set; }
    }
}
