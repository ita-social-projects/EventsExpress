using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Authorization;

namespace EventsExpress.Policies
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; set; }
    }
}
