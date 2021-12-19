using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Core.GraphQL.IDataLoaders;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Ignore(f => f.CategoryGroupId);

            // ?
            descriptor.Ignore(f => f.CategoryGroup);

            descriptor
                .Field(f => f.Events)
                .ResolveWith<CategoryResolvers>(r => r.GetEventsAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("events");

            descriptor
                .Field(f => f.Users)
                .ResolveWith<CategoryResolvers>(r => r.GetUsersAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("users");
        }

        private class CategoryResolvers
        {
            public async Task<IEnumerable<Event>> GetEventsAsync([Parent] Category category, [ScopedService] AppDbContext dbContext, IEventByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] eventIds = await dbContext.Categories
                    .Where(a => a.Id == category.Id)
                    .Include(a => a.Events)
                    .SelectMany(a => a.Events.Select(t => t.EventId))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(eventIds, cancellationToken);
            }

            public async Task<IEnumerable<User>> GetUsersAsync([Parent] Category category, [ScopedService] AppDbContext dbContext, IUserByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] userIds = await dbContext.Categories
                    .Where(a => a.Id == category.Id)
                    .Include(a => a.Users)
                    .SelectMany(a => a.Users.Select(t => t.UserId))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(userIds, cancellationToken);
            }
        }
    }
}
