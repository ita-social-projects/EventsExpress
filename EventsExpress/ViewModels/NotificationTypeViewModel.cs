namespace EventsExpress.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventsExpress.Db.Enums;

    public class NotificationTypeViewModel
    {
        public NotificationChange Id { get; set; }

        public string Name { get; set; }
    }
}
