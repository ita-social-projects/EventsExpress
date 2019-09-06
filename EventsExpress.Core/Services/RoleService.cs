using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System.Collections.Generic;
using System.Linq;

namespace EventsExpress.Core.Services
{
    public class RoleService : IRoleService
    {
        public IUnitOfWork Db { get; set; }

        public RoleService(IUnitOfWork uow)
        {
            Db = uow;
        }

        public IEnumerable<Role> All() => Db.RoleRepository.Get().AsEnumerable();
        
    }
}
