using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.DataLoaders
{
    public class EventByIdDataLoader : BatchDataLoader<Guid, Event>
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public EventByIdDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions options, IDbContextFactory<AppDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Event>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using AppDbContext dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Events
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
