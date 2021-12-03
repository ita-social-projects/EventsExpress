using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventScheduleType : ObjectType<EventSchedule>
    {
        protected override void Configure(IObjectTypeDescriptor<EventSchedule> descriptor)
        {
            descriptor.Field(f => f.Id).Type<StringType>();
            descriptor.Field(f => f.LastRun).Type<DateTimeType>();
            descriptor.Field(f => f.NextRun).Type<DateTimeType>();

            // descriptor.Field(f => f.Periodicity).Type
            descriptor.Field(f => f.IsActive).Type<BooleanType>();
            descriptor.Field(f => f.EventId).Type<IdType>();
            descriptor.Field(f => f.Event).Type<EventType>();
        }
    }
}
