using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Spatial;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            // descriptor
            //    .ImplementsNode()
            //    .IdField(f => f.Id)
            //    .ResolveNode((context, id) => context.DataLoader<EventByIdDataLoader>().LoadAsync(id, context.RequestAborted));
            descriptor
                .Field(f => f.Title)
                .Type<StringType>();

            descriptor
                .Field(f => f.Description)
                .Type<StringType>();

            descriptor
                .Field(f => f.DateFrom)
                .Type<DateTimeType>();

            descriptor
                .Field(f => f.DateTo)
                .Type<DateTimeType>();

            descriptor
                .Field(f => f.IsPublic)
                .Type<BooleanType>();

            descriptor
                .Field(f => f.MaxParticipants)
                .Type<IntType>();

            descriptor
                .Field(f => f.EventLocationId)
                .Type<UuidType>();

            descriptor
                .Field(f => f.Categories)
                .ResolveWith<EventResolvers>(r => r.GetCategoriesAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .Name("categories");

            descriptor
                .Field(f => f.EventLocation)
                .ResolveWith<EventResolvers>(r => r.GetEventLocationsAsync(default, default, default));

            descriptor
                .Field(f => f.EventSchedule)
                .ResolveWith<EventResolvers>(r => r.GetEventSchedulesAsync(default, default, default));

            descriptor.Ignore(f => f.Inventories);
            descriptor.Ignore(f => f.Owners);
            descriptor.Ignore(f => f.Rates);
            descriptor.Ignore(f => f.StatusHistory);

            descriptor.Ignore(f => f.Visitors);
        }

        private class EventResolvers
        {
            public async Task<IEnumerable<Category>> GetCategoriesAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, CategoryByIdDataLoader categoryById, CancellationToken cancellationToken)
            {
                Guid[] categoriesId = await dbContext.Events
                    .Where(s => s.Id == ev.Id)
                    .Include(s => s.Categories)
                    .SelectMany(s => s.Categories.Select(t => t.CategoryId))
                    .ToArrayAsync();

                return await categoryById.LoadAsync(categoriesId, cancellationToken);
            }

            public async Task<EventLocation> GetEventLocationsAsync([Parent] Event ev, EventLocationByIdDataLoader eventLocationById, CancellationToken cancellationToken)
            {
                if (ev.EventLocationId is null)
                {
                    return null;
                }

                return await eventLocationById.LoadAsync(ev.EventLocationId.Value, cancellationToken);
            }

            public async Task<EventSchedule> GetEventSchedulesAsync([Parent] Event ev, EventScheduleByEventIdDataLoader eventScheduleByEventId, CancellationToken cancellationToken)
            {
                if (ev.EventSchedule is null)
                {
                    return null;
                }

                return await eventScheduleByEventId.LoadAsync(ev.EventSchedule.Id, cancellationToken);
            }
        }
    }
}
