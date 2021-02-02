using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Filters
{
    public class UserAccessTypeFilterAttribute : TypeFilterAttribute
    {
        public UserAccessTypeFilterAttribute(string eventId = "eventId")
            : base(typeof(UserAccessFilterAttribute))
        {
            Arguments = new object[] { eventId };
        }
    }
}
