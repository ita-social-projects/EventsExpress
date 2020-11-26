using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Role> All() => Get().AsEnumerable();
    }
}
