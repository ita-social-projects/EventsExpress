using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface IRoleService
    {
        IEnumerable<Role> All();
    }
}
