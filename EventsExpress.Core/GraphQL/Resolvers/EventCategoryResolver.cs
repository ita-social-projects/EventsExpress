using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate.Resolvers;

namespace EventsExpress.Core.GraphQL.Resolvers
{
    public class EventCategoryResolver
    {
        private readonly AppDbContext dbContext;

        public EventCategoryResolver(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // public IQueryable<EventCategoryResolver> GetCategories(Event ev, IResolverContext context)
        // {
        //    return dbContext.
        // }
    }
}
