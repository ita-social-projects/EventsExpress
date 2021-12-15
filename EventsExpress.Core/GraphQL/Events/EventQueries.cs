using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Core.GraphQL.Types;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Events
{
    [ExtendObjectType("Query")]
    public class EventQueries
    {
        [UseApplicationDbContext]
        public Task<List<Event>> GetEvents([ScopedService] AppDbContext context) =>
            context.Events.ToListAsync();

        public Task<Event> GetEventByIdAsync([ID(nameof(Event))] Guid id, EventByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
               dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Event>> GetEventsByIdAsync([ID(nameof(Event))] Guid[] ids, EventByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);

        // [UseApplicationDbContext]
        // public async Task<IEnumerable<Event>> GetEventsAsync(
        //    [ScopedService] AppDbContext appDbContext,
        //    CancellationToken cancellationToken)
        // {
        //    return await appDbContext.Events.ToListAsync(cancellationToken);
        // }
    }
}
