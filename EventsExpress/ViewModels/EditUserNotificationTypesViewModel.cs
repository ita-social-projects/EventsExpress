namespace EventsExpress.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventsExpress.Core.DTOs;

    public class EditUserNotificationTypesViewModel
    {
        public IEnumerable<NotificationTypeDTO> NotificationTypes { get; set; }
    }
}
