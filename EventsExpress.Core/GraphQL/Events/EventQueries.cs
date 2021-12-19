using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Core.GraphQL.IDataLoaders;
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
        private readonly IEventByIdDataLoader eventDataLoader;

        public EventQueries(IEventByIdDataLoader eventDataLoader)
        {
            this.eventDataLoader = eventDataLoader;
        }

        [UseApplicationDbContext]
        public Task<List<Event>> GetEvents([ScopedService] AppDbContext context) => context.Events.ToListAsync();

        public Task<Event> GetEventByIdAsync([ID(nameof(Event))] Guid id, IEventByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
               dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Event>> GetEventsByIdAsync([ID(nameof(Event))] Guid[] ids, IEventByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);
    }
}
