using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventCategoryType : ObjectType<EventCategory>
    {
        protected override void Configure(IObjectTypeDescriptor<EventCategory> descriptor)
        {
            descriptor.Field(f => f.EventId).Type<IdType>();
            descriptor.Field(f => f.Event).Type<EventType>();
            descriptor.Field(f => f.CategoryId).Type<IdType>();
            descriptor.Field(f => f.Category).Type<CategoryType>();
        }
    }
}
