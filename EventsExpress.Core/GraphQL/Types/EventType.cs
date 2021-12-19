using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.IDataLoaders;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor.Field(f => f.Title).Type<StringType>();
            descriptor.Field(f => f.Description).Type<StringType>();
            descriptor.Field(f => f.DateFrom).Type<DateTimeType>();
            descriptor.Field(f => f.DateTo).Type<DateTimeType>();
            descriptor.Field(f => f.IsPublic).Type<BooleanType>();
            descriptor.Field(f => f.MaxParticipants).Type<IntType>();
            descriptor.Field(f => f.EventLocationId).Type<UuidType>();

            descriptor
                .Field(f => f.EventSchedule)
                .ResolveWith<EventResolvers>(r => r.GetEventScheduleAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("eventSchedule");

            descriptor
                .Field(f => f.EventLocation)
                .ResolveWith<EventResolvers>(r => r.GetEventLocationsAsync(default, default, default));

            descriptor
                .Field(f => f.Owners)
                .ResolveWith<EventResolvers>(r => r.GetOwnersAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("owners");

            descriptor
                .Field(f => f.Visitors)
                .UseDbContext<AppDbContext>()
                .Name("visitors");

            descriptor
                .Field(f => f.Categories)
                .ResolveWith<EventResolvers>(r => r.GetCategoriesAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("categories");

            descriptor
                .Field(f => f.Rates)
                .ResolveWith<EventResolvers>(r => r.GetRatesAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("rates");

            descriptor
                .Field(f => f.Inventories)
                .ResolveWith<EventResolvers>(r => r.GetInventoriesAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("inventories");

            descriptor
                .Field(f => f.StatusHistory)
                .ResolveWith<EventResolvers>(r => r.GetEventStatusHistoryAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("statusHistory");
        }

        private class EventResolvers
        {
            public async Task<IEnumerable<EventSchedule>> GetEventScheduleAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IEventScheduleByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] eventScheduleIds = await dbContext.Events
                   .Where(s => s.Id == ev.Id && s.EventSchedule != null)
                   .Include(s => s.EventSchedule)
                   .Select(s => s.EventSchedule.Id)
                   .ToArrayAsync();

                return await dataLoader.LoadAsync(eventScheduleIds, cancellationToken);
            }

            public async Task<EventLocation> GetEventLocationsAsync([Parent] Event ev, IEventLocationByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                if (ev.EventLocationId is null)
                {
                    return null;
                }

                return await dataLoader.LoadAsync(ev.EventLocationId.Value, cancellationToken);
            }

            public async Task<IEnumerable<User>> GetOwnersAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IUserByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] userIds = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.Owners)
                    .SelectMany(s => s.Owners.Select(t => t.UserId))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(userIds, cancellationToken);
            }

            public async Task<IEnumerable<User>> GetVisitorsAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IUserByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] visitorIds = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.Visitors)
                    .SelectMany(s => s.Visitors.Select(t => t.UserId))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(visitorIds, cancellationToken);
            }

            public async Task<IEnumerable<Category>> GetCategoriesAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, ICategoryByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] categoryIds = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.Categories)
                    .SelectMany(s => s.Categories.Select(t => t.CategoryId))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(categoryIds, cancellationToken);
            }

            public async Task<IEnumerable<Rate>> GetRatesAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IRateByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] rateIds = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.Rates)
                    .SelectMany(s => s.Rates.Select(t => t.Id))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(rateIds, cancellationToken);
            }

            public async Task<IEnumerable<Inventory>> GetInventoriesAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IInventoryByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] inventoryIds = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.Inventories)
                    .SelectMany(s => s.Inventories.Select(t => t.Id))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(inventoryIds, cancellationToken);
            }

            public async Task<IEnumerable<EventStatusHistory>> GetEventStatusHistoryAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, IEventStatusHistoryByIdDataLoader dataLoader, CancellationToken cancellationToken)
            {
                Guid[] statusHistoryIds = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.StatusHistory)
                    .SelectMany(s => s.StatusHistory.Select(t => t.Id))
                    .ToArrayAsync();

                return await dataLoader.LoadAsync(statusHistoryIds, cancellationToken);
            }
        }
    }
}
