using EventsExpress.Core.GraphQL.Resolvers;
using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor.Field(f => f.Title).Type<StringType>();
            descriptor.Field(f => f.Description).Type<StringType>();
            descriptor.Field(f => f.DateFrom).Type<DateTimeType>();
            descriptor.Field(f => f.DateTo).Type<DateTimeType>();
            descriptor.Field(f => f.IsPublic).Type<BooleanType>();
            descriptor.Field(f => f.MaxParticipants).Type<IntType>();
            descriptor.Field(f => f.EventLocationId).Type<IdType>();
            descriptor.Field(f => f.EventSchedule).Type<EventScheduleType>();

                // .ResolveWith<EventScheduleResolvers>(r => r.GetEventSchedules(default, default));
            descriptor.Field(f => f.EventLocation).Type<EventLocationType>();
            descriptor.Field(f => f.Owners).Type<StringType>();
            descriptor.Field(f => f.Visitors).Type<StringType>();

            descriptor.Field(f => f.Categories).Type<ListType<EventCategoryType>>();

            descriptor.Field(f => f.Rates).Type<StringType>();
            descriptor.Field(f => f.Inventories).Type<StringType>();
            descriptor.Field(f => f.StatusHistory).Type<StringType>();
        }
    }
}
