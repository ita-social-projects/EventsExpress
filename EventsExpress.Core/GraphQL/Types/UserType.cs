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
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .Field(f => f.Name)
                .Type<StringType>();

            descriptor.Ignore(f => f.Account);
            descriptor.Ignore(f => f.Email);
            descriptor.Ignore(f => f.Phone);
            descriptor.Ignore(f => f.Birthday);
            descriptor.Ignore(f => f.Gender);
            descriptor.Ignore(f => f.Events);
            descriptor.Ignore(f => f.EventsToVisit);
            descriptor.Ignore(f => f.Categories);
            descriptor.Ignore(f => f.Rates);
            descriptor.Ignore(f => f.Relationships);
            descriptor.Ignore(f => f.Chats);
            descriptor.Ignore(f => f.ChangedStatusEvents);
            descriptor.Ignore(f => f.NotificationTypes);
        }
    }
}
