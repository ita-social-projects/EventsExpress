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
        // [AllowAnonymous]
        [Serial]
        public IQueryable<Event> GetEvents([Service] AppDbContext context)
        {
            return context.Events.AsQueryable();
        }

        // public TestItem GetTestItems() => new TestItem { Id = 1, Name = "Test Item" };

        // public TestItem GetOneTestItem() => new TestItem { Id = 2, Name = "Test Item2" };
    }
}
