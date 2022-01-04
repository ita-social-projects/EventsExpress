using System.Linq;
using EventsExpress.Core.GraphQL.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Services
{
    public class EventGraphQLService : IEventGraphQLService
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public EventGraphQLService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public IQueryable<Event> GetEvents()
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            {
                return dbContext.Events;
            }
        }
    }
}
