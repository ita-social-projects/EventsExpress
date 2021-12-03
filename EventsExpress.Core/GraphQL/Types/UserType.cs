using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    internal class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.Account).Type<AccountType>();
            descriptor.Field(f => f.Email).Type<StringType>();
            descriptor.Field(f => f.Phone).Type<StringType>();
            descriptor.Field(f => f.Birthday).Type<DateTimeType>();
            descriptor.Field(f => f.Gender).Type<GenderEnumType>();

            descriptor.Field(f => f.Events).Type<StringType>();
            descriptor.Field(f => f.EventsToVisit).Type<StringType>();
            descriptor.Field(f => f.Categories).Type<StringType>();
            descriptor.Field(f => f.Rates).Type<StringType>();
            descriptor.Field(f => f.Relationships).Type<StringType>();
            descriptor.Field(f => f.Chats).Type<StringType>();
            descriptor.Field(f => f.ChangedStatusEvents).Type<StringType>();
            descriptor.Field(f => f.NotificationTypes).Type<StringType>();
        }
    }
}
