using System;
using System.Collections.Generic;

namespace EventsExpress.ViewModels
{
    public class ChangeRoleWiewModel
    {
        public Guid UserId { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}
