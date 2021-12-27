using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor.Field(f => f.Name)
                .Type<StringType>();

            descriptor.Field(f => f.CategoryGroupId)
                .Type<UuidType>();

            descriptor.Ignore(f => f.CategoryGroup);
            descriptor.Ignore(f => f.Users);
            descriptor.Ignore(f => f.Events);
        }
    }
}
