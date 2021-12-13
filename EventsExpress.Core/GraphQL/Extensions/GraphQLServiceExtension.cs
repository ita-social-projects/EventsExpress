using System;
using EventsExpress.Core.GraphQL.Categories;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Core.GraphQL.Events;
using EventsExpress.Core.GraphQL.EventSchedules;
using EventsExpress.Core.GraphQL.Types;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.GraphQL.Extensions
{
    public static class GraphQLServiceExtension
    {
        public static void AddGraphQLService(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                    .AddTypeExtension<EventQueries>()
                    .AddTypeExtension<CategoryQueries>()
                    .AddTypeExtension<EventScheduleQueries>()
                .AddType<EventType>()
                .AddType<CategoryType>()
                .AddType<UserType>()
                .AddType<EventScheduleType>()
                .AddType<InventoryType>()

                // .AddType<EventLocationType>()
                .AddSpatialTypes()
                .AddDataLoader<EventByIdDataLoader>()
                .AddDataLoader<CategoryByIdDataLoader>()
                .AddDataLoader<UserByIdDataLoader>()
                .AddDataLoader<EventLocationByIdDataLoader>();

                // .ModifyOptions(options => options.DefaultResolverStrategy = HotChocolate.Execution.ExecutionStrategy.Serial)
                // .AddProjections()
                // .AddErrorFilter(error =>
                // {
                //    switch (error.Exception)
                //    {
                //        case ArgumentException argEx:
                //            return ErrorBuilder.FromError(error)
                //                .SetMessage(argEx.Message)
                //                .SetCode("ArgumentException")
                //                .RemoveException()
                //                .ClearExtensions()
                //                .ClearLocations()
                //                .Build();
                //        case HotChocolate.SchemaException shcEr:
                //            return ErrorBuilder.FromError(error)
                //                .SetMessage(shcEr.Message)
                //                .SetCode("Schema exception")
                //                .Build();
                //    }
                //    return error;
                // });
        }
    }
}
