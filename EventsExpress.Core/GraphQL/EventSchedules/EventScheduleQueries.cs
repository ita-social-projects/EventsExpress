using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.EventSchedules
{
    [ExtendObjectType("Query")]
    public class EventScheduleQueries
    {
        [UseApplicationDbContext]
        public Task<List<EventSchedule>> GetEventSchedules([ScopedService] AppDbContext context) => context.EventSchedules.ToListAsync();
    }
}
