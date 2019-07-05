using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class AuthServicre : IAuthServicre
    {
        // private readonly IUnitOfWork _uow
        public AuthServicre()
        {
            //_uow = uow;
        }


        public Task<bool> AuthenticateAsync(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
