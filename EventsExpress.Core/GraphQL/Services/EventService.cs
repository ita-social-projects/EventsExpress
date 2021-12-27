using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.GraphQL.Services
{
    public class EventService : IEventService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public EventService(IDbContextFactory<AppDbContext> dbContextFactory, IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IQueryable<Event>> GetEvents()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                List<Event> result = await dbContext.Events.ToListAsync();

                return result.AsQueryable();
            }

            // using (var dbContext = dbContextFactory.CreateDbContext())
            // {
            //    return await dbContext.Events.ToListAsync();
            // }
        }
    }
}
