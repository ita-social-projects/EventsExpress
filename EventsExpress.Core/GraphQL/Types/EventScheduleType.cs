using EventsExpress.Db.Entities;
using HotChocolate.Types;

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

            descriptor.Ignore(f => f.EventId);
            descriptor.Ignore(f => f.Event);
        }
    }
}
