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
    public class EventStatusHistoryType : ObjectType<EventStatusHistory>
    {
        protected override void Configure(IObjectTypeDescriptor<EventStatusHistory> descriptor)
        {
            descriptor.Field(f => f.UserId).Type<UuidType>();
            descriptor.Ignore(f => f.User);
            descriptor.Field(f => f.EventId);
            descriptor.Ignore(f => f.Event);
            descriptor.Ignore(f => f.EventStatus);
            descriptor.Field(f => f.Reason).Type<StringType>();
            descriptor.Field(f => f.CreatedOn).Type<DateTimeType>();
        }
    }
}
