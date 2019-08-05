using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsExpress.Core.Services
{
    public class RoleService : IRoleService
    {
        public IUnitOfWork Db { get; set; }
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork uow, IMapper mapper)
        {
            Db = uow;
            _mapper = mapper;
        }


        public IEnumerable<Role> All()
        {
            return Db.RoleRepository.Get().AsEnumerable();
        }
    }
}
