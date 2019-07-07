using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IAuthServicre
    {
        OperationResult Authenticate(string email, string password);
    }
}
