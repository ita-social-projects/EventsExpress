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
    public class EventLocationByIdDataLoader : BatchDataLoader<Guid, EventLocation>
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public EventLocationByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<AppDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ?? throw new ArgumentException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, EventLocation>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using AppDbContext dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.EventLocations
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
