using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.GraphQL.IServices
{
    public interface IEventService
    {
        public Task<IQueryable<Event>> GetEvents();
    }
}
