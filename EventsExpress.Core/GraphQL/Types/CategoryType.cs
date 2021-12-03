using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    internal class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.Users).Type<ListType<UserType>>();
            descriptor.Field(f => f.Events).Type<EventCategoryType>();
        }
    }
}
