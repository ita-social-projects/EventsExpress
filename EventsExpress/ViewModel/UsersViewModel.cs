using EventsExpress.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.ViewModel
{
    public class UsersViewModel
    {
        public IEnumerable<UserManageDto> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
