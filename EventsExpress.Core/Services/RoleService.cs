using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class RoleService : IRoleService
    {
        public RoleService(IUnitOfWork uow)
        {
            Db = uow;
        }

        public IUnitOfWork Db { get; set; }

        public IEnumerable<Role> All() => Db.RoleRepository.Get().AsEnumerable();
    }
}
