using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class AuthDto
    {
        public string Email { get; set; }

        public AuthExternalType? Type { get; set; }
    }
}
