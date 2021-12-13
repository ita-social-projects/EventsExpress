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
    public class EventScheduleType : ObjectType<EventSchedule>
    {
        protected override void Configure(IObjectTypeDescriptor<EventSchedule> descriptor)
        {
            descriptor.Field(f => f.Frequency).Type<IntType>();
            descriptor.Field(f => f.LastRun).Type<DateTimeType>();
            descriptor.Field(f => f.NextRun).Type<DateTimeType>();
            descriptor.Field(f => f.Periodicity).Type<PeriodicityEnumType>();
            descriptor.Field(f => f.IsActive).Type<BooleanType>();
            descriptor.Field(f => f.EventId).Type<UuidType>();

            descriptor
                .Field(f => f.Event)
                .ResolveWith<EventScheduleResolvers>(r => r.GetEventsAsync(default, default, default, default));
        }

        private class EventScheduleResolvers
        {
            public async Task<IEnumerable<Event>> GetEventsAsync([Parent] Event ev, [ScopedService] AppDbContext dbContext, EventByIdDataLoader eventById, CancellationToken cancellationToken)
            {
                Guid[] eventIds = await dbContext.EventSchedules
                    .Where(a => a.EventId == ev.Id)
                    .Include(a => a.Event)
                    .Select(a => a.EventId)
                    .ToArrayAsync();

                return await eventById.LoadAsync(eventIds, cancellationToken);
            }
        }
    }
}
