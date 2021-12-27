using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate.Data;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(f => f.GetEvents())
                .Name("events");
        }
    }
}
