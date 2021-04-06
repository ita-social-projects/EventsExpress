using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class ChangeRoleWiewModel
    {
        public Guid UserId { get; set; }

        public IEnumerable<AccountRole> Roles { get; set; } // todo RoleWiewModel
    }
}
