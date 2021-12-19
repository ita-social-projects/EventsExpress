using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class RateType : ObjectType<Rate>
    {
        protected override void Configure(IObjectTypeDescriptor<Rate> descriptor)
        {
            descriptor.Field(f => f.UserFromId).Type<UuidType>();
            descriptor.Ignore(f => f.UserFrom);
            descriptor.Field(f => f.EventId).Type<UuidType>();
            descriptor.Ignore(f => f.Event);
            descriptor.Field(f => f.Score).Type<ByteType>();
        }
    }
}
