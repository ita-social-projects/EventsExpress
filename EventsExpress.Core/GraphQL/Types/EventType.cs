using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor.Field(f => f.Title);
            descriptor.Field(f => f.Description);
            descriptor.Field(f => f.DateFrom);
            descriptor.Field(f => f.DateTo);
            descriptor.Field(f => f.IsPublic);
            descriptor.Field(f => f.MaxParticipants);
            descriptor.Field(f => f.LocationId);
            descriptor.Field(f => f.EventAudienceId);
            descriptor.Field(f => f.EventAudience);
            descriptor.Field(f => f.EventSchedule);
            descriptor.Field(f => f.Location);
            descriptor.Field(f => f.Organizers);
            descriptor.Field(f => f.Visitors);
            descriptor.Field(f => f.Categories);
            descriptor.Field(f => f.Rates);
            descriptor.Field(f => f.Inventories);
            descriptor.Field(f => f.StatusHistory);
        }
    }
}
