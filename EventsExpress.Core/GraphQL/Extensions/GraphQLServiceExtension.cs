using System;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Core.GraphQL.Events;
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
                    .AddType<EventType>()
                .AddDataLoader<EventByIdDataLoader>();

                // .AddSpatialTypes()

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
