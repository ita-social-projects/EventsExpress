using System.Collections.Generic;
using System.Linq;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.NotificationTemplateModels
{
    public class RoleChangeNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserEmail { get; set; }

        public IEnumerable<AccountRole> Roles { get; set; }

        public string FormattedRoles
        {
            get
            {
                string delimiter = ", ";
                return Roles.Select(it => it.RoleId.ToString())
                    .Aggregate((result, nextItem) => result + delimiter + nextItem);
            }
        }
    }
}
