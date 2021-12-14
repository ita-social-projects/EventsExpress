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
    public class RateType : ObjectType<Rate>
    {
        protected override void Configure(IObjectTypeDescriptor<Rate> descriptor)
        {
            descriptor
                .Field(f => f.UserFromId)
                .Type<UuidType>();

            descriptor.Ignore(f => f.UserFrom);

            descriptor
                .Field(f => f.EventId)
                .Type<UuidType>();

            descriptor.Ignore(f => f.Event);

            descriptor
                .Field(f => f.Score)
                .Type<ByteType>();
        }
    }
}
