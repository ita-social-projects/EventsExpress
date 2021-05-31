using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class UpdateIssueStatusViewModel
    {
        public Guid MessageId { get; set; }

        public ContactAdminStatus Status { get; set; }

        public string ResolutionDetails { get; set; }
    }
}
