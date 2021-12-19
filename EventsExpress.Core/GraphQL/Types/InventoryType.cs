using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class InventoryType : ObjectType<Inventory>
    {
        protected override void Configure(IObjectTypeDescriptor<Inventory> descriptor)
        {
            descriptor.Field(f => f.NeedQuantity).Type<DecimalType>();
            descriptor.Field(f => f.ItemName).Type<StringType>();
            descriptor.Field(f => f.EventId).Type<UuidType>();

            descriptor.Ignore(f => f.Event);
            descriptor.Ignore(f => f.UnitOfMeasuringId);
            descriptor.Ignore(f => f.UnitOfMeasuring);
            descriptor.Ignore(f => f.UserEventInventories);
        }
    }
}
