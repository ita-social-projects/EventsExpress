using EventsExpress.Db.Enums;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class LocationTypeEnumType : EnumType<LocationType>
    {
        protected override void Configure(IEnumTypeDescriptor<LocationType> descriptor)
        {
            descriptor.BindValues(BindingBehavior.Implicit);
        }
    }
}
