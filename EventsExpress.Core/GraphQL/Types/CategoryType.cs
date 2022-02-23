using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.CategoryGroupId).Type<UuidType>();
            descriptor.Field(f => f.CategoryGroup);
            descriptor.Field(f => f.Users);
            descriptor.Field(f => f.Events);
        }
    }
}
