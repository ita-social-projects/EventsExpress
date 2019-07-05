using EventsExpress.Db.Entities;     
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Db.IRepo
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
    }
}
