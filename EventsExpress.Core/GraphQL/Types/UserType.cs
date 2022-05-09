using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(f => f.Name);
            descriptor.Ignore(f => f.Account);
            descriptor.Ignore(f => f.Birthday);
            descriptor.Ignore(f => f.Categories);
            descriptor.Ignore(f => f.ChangedStatusEvents);
            descriptor.Ignore(f => f.Chats);
            descriptor.Ignore(f => f.Email);
            descriptor.Ignore(f => f.Events);
            descriptor.Ignore(f => f.EventsToVisit);
            descriptor.Ignore(f => f.Gender);
            descriptor.Ignore(f => f.NotificationTypes);
            descriptor.Ignore(f => f.Phone);
            descriptor.Ignore(f => f.Rates);
            descriptor.Ignore(f => f.Relationships);
        }
    }
}
