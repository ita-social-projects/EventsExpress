using System.Linq;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.GraphQL.IServices
{
    public interface IEventGraphQLService
    {
        public IQueryable<Event> GetEvents();
    }
}
