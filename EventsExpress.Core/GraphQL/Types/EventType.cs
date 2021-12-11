using System;
using System.Collections;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(f => f.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<EventByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor.Field(f => f.Title).Type<StringType>();

            descriptor.Ignore(f => f.Categories);
            descriptor.Ignore(f => f.Description);
            descriptor.Ignore(f => f.DateFrom);
            descriptor.Ignore(f => f.DateTo);
            descriptor.Ignore(f => f.EventLocation);
            descriptor.Ignore(f => f.EventSchedule);
            descriptor.Ignore(f => f.EventLocationId);
            descriptor.Ignore(f => f.Inventories);
            descriptor.Ignore(f => f.IsPublic);
            descriptor.Ignore(f => f.MaxParticipants);
            descriptor.Ignore(f => f.Owners);
            descriptor.Ignore(f => f.Rates);
            descriptor.Ignore(f => f.StatusHistory);
            descriptor.Ignore(f => f.Visitors);

            // descriptor
            //    .ImplementsNode()
            //    .IdField(t => t.Id)
            //    .ResolveNode((context, id) => context.DataLoader<EventByIdDataLoader>().LoadAsync(id, context.RequestAborted));

            // descriptor
            //    .Field(t => t.Title)
            //    .Type<StringType>()
            //    .Name("title");
        }
    }
}
