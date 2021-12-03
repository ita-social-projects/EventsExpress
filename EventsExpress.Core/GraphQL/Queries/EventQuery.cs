using System.Collections.Generic;
using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Queries
{
    public class EventQuery
    {
        [UseProjection]
        public IQueryable<Event> GetEvents([Service] AppDbContext context) => context.Events;
    }
}
