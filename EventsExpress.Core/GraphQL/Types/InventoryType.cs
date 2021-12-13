using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Spatial;
using Microsoft.EntityFrameworkCore;

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
            descriptor.Ignore(f => f.UserEventInventories);
        }
    }
}
