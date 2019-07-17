using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class UserInfo
    {
        public Guid Id;
        public string Name;
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public byte Gender { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
        public string Token { get; set; }

    }
}
