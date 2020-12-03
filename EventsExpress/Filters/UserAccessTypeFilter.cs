using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Filters
{
    public class UserAccessTypeFilter : TypeFilterAttribute
    {
        public UserAccessTypeFilter(string eventId = "eventId")
            : base(typeof(UserAccessFilter))
        {
            Arguments = new object[] { eventId };
        }
    }
}
