using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    interface IAuthServicre
    {
        Task<bool> AuthenticateAsync(string name, string password);
        Task SignOutAsync();
    }
}
