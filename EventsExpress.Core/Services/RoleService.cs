using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _db;

        public RoleService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public IEnumerable<Role> All() => _db.RoleRepository.Get().AsEnumerable();
    }
}
