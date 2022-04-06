using EventsExpress.Db.EF;
using HotChocolate.Data;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor
                .Field("events")
                .Type<ListType<EventType>>()
                .UseDbContext<AppDbContext>()
                .Resolve(resolver =>
                {
                    return resolver.DbContext<AppDbContext>().Events;
                })

                .UsePaging<EventType>()
                .UseProjection()
                .UseFiltering()
                .UseSorting();
        }
    }
}
