using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate.Resolvers;

namespace EventsExpress.Core.GraphQL.Resolvers
{
    public class EventScheduleResolvers
    {
        private readonly AppDbContext dbContext;

        public EventScheduleResolvers(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<EventSchedule> GetEventSchedules(Event ev, IResolverContext context)
        {
            return dbContext.EventSchedules
                .Where(evSchedule => evSchedule.Id == ev.Id);
        }
    }
}
