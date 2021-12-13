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
    public class CategoryByIdDataLoader : BatchDataLoader<Guid, Category>
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public CategoryByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<AppDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ?? throw new ArgumentException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Category>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using AppDbContext dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Categories
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
