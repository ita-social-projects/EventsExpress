using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Core.GraphQL.SortInputTypes;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field("events")
                .Type<ListType<EventType>>()
                .UseDbContext<AppDbContext>()
                .Resolve(resolver =>
                {
                    return resolver.DbContext<AppDbContext>().Events;
                })

                .UsePaging<EventType>()
                .UseProjection()
                .UseFiltering()
                .UseSorting();
        }
    }
}
