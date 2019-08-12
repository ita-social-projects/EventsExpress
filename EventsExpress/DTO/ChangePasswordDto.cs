using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class ChangePasswordDto
    {
        
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
