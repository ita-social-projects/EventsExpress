using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventLocationType : ObjectType<EventLocation>
    {
        protected override void Configure(IObjectTypeDescriptor<EventLocation> descriptor)
        {
            descriptor.Field(f => f.OnlineMeeting);
            descriptor.Field(f => f.Point);
            descriptor.Field(f => f.Type);
        }
    }
}
