using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class AuthViewModel
    {
        public string Email { get; set; }

        public AuthExternalType? Type { get; set; }
    }
}
