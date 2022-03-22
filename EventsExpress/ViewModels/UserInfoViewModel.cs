using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels.Base;

namespace EventsExpress.ViewModels
{
    public class UserInfoViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public byte Gender { get; set; }

        public LocationViewModel Location { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<NotificationTypeViewModel> NotificationTypes { get; set; }

        public double Rating { get; set; }

        public IEnumerable<Guid> BookmarkedEvents { get; set; }

        public bool CanChangePassword { get; set; }
    }
}
