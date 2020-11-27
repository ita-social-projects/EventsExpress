using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(AppDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Role> All() => _context.Roles.AsEnumerable();
    }
}
