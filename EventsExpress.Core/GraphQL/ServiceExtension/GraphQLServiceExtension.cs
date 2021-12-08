using System;
using EventsExpress.Core.GraphQL.Queries;
using EventsExpress.Core.GraphQL.Types;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.GraphQL.ServiceExtension
{
    public static class GraphQLServiceExtension
    {
        public static void AddGraphQLService(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()

                .ModifyOptions(options => options.DefaultBindingBehavior = HotChocolate.Types.BindingBehavior.Explicit)
                .AddSpatialTypes()
                .AddType<EventQueryType>()
                .AddType<EventType>()
                .AddType<EventScheduleType>()
                .AddType<EventCategoryType>()
                .AddType<EventLocationType>()

                .AddQueryType<EventQuery>()

                // .ModifyOptions(options => options.DefaultResolverStrategy = HotChocolate.Execution.ExecutionStrategy.Serial)
                .AddProjections()
                .AddErrorFilter(error =>
                {
                    switch (error.Exception)
                    {
                        case ArgumentException argEx:
                            return ErrorBuilder.FromError(error)
                                .SetMessage(argEx.Message)
                                .SetCode("ArgumentException")
                                .RemoveException()
                                .ClearExtensions()
                                .ClearLocations()
                                .Build();
                        case HotChocolate.SchemaException shcEr:
                            return ErrorBuilder.FromError(error)
                                .SetMessage(shcEr.Message)
                                .SetCode("Schema exception")
                                .Build();
                    }

                    return error;
                });
        }
    }
}
