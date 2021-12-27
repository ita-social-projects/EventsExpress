using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Core.GraphQL.IDataLoaders;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using GreenDonut;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            // descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Title)
                .Type<StringType>();

            descriptor.Field(f => f.Description)
                .Type<StringType>();

            descriptor.Field(f => f.DateFrom)
                .Type<DateTimeType>();

            descriptor.Field(f => f.DateTo)
                .Type<DateTimeType>();

            descriptor.Ignore(f => f.IsPublic);

            descriptor.Field(f => f.MaxParticipants)
                .Type<IntType>();

            descriptor.Field(f => f.EventLocationId)
                .Type<UuidType>();

            descriptor.Field(f => f.Categories)
                .ResolveWith<EventResolvers>(r => r.GetCategoriesAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("categories");

            descriptor.Field(f => f.EventSchedule)
                .ResolveWith<EventResolvers>(r => r.GetEventSchedulesAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("eventSchedule");

            descriptor.Ignore(f => f.EventLocation);

            descriptor.Ignore(f => f.Owners);

            descriptor.Ignore(f => f.Visitors);

            descriptor.Ignore(f => f.Rates);

            descriptor.Ignore(f => f.Inventories);

            descriptor.Ignore(f => f.StatusHistory);
        }

        private class EventResolvers
        {
            public async Task<IQueryable<Category>> GetCategoriesAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, ICategoryByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] categoryIds = await dbContext.Events
                    .Where(e => e.Id == ev.Id)
                    .Include(e => e.Categories)
                    .SelectMany(s => s.Categories.Select(t => t.CategoryId))
                    .ToArrayAsync();

                IReadOnlyList<Category> categories = await dataLoader.LoadAsync(categoryIds, cancellationToken);

                return categories.AsQueryable<Category>();

                // var result = await dbContext.Events
                //   .Where(e => e.Id == ev.Id)
                //   .SelectMany(c => c.Categories)
                //   .Select(c => c.Category)
                //   .ToListAsync();

                // return result.AsQueryable();
            }

            public async Task<IQueryable<EventSchedule>> GetEventSchedulesAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IResolverContext context, CancellationToken cancellationToken)
            {
                var result = await dbContext.Events
                    .Where(e => e.Id == ev.Id)
                    .Select(e => e.EventSchedule)
                    .ToListAsync();

                return result.AsQueryable();
            }
        }
    }
}
