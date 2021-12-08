using EventsExpress.Db.Enums;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class GenderEnumType : EnumType<Gender>
    {
        protected override void Configure(IEnumTypeDescriptor<Gender> descriptor)
        {
            descriptor.BindValues(BindingBehavior.Implicit);
        }
    }
}
