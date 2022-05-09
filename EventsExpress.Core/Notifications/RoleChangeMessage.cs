using System.Collections.Generic;
using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class RoleChangeMessage : INotification
    {
        public RoleChangeMessage(Account account, IEnumerable<AccountRole> roles)
        {
            Account = account;
            Roles = roles;
        }

        public Account Account { get; set; }

        public IEnumerable<AccountRole> Roles { get; set; }
    }
}
