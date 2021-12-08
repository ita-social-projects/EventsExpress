using System.Collections.Generic;
using EventsExpress.Core.GraphQL.Queries;
using EventsExpress.Core.GraphQL.Resolvers;
using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventQueryType : ObjectType<EventQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<EventQuery> descriptor)
        {
            descriptor.Field(f => f.GetEvents)
                .Name("events")
                .Type<EventType>();
        }
    }
}
