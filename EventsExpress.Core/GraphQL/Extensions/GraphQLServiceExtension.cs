using System;
using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Core.GraphQL.Events;
using EventsExpress.Core.GraphQL.IDataLoaders;
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
                .AddType<CategoryType>()
                .AddType<UserType>()
                .AddType<EventScheduleType>()
                .AddType<InventoryType>()

                // .AddType<EventLocationType>()
                .AddSpatialTypes()
                .AddDataLoader<IEventByIdDataLoader,              EventByIdDataLoader>()
                .AddDataLoader<ICategoryByIdDataLoader,           CategoryByIdDataLoader>()
                .AddDataLoader<IUserByIdDataLoader,               UserByIdDataLoader>()
                .AddDataLoader<IEventScheduleByIdDataLoader,      EventScheduleByIdDataLoader>()
                .AddDataLoader<IEventLocationByIdDataLoader,      EventLocationByIdDataLoader>()
                .AddDataLoader<IInventoryByIdDataLoader,          InventoryByIdDataLoader>()
                .AddDataLoader<IRateByIdDataLoader,               RateByIdDataLoader>()
                .AddDataLoader<IEventStatusHistoryByIdDataLoader, EventStatusHistoryByIdDataLoader>()

                .AddErrorFilter(error =>
                {
                    switch (error.Exception)
                    {
                        case ArgumentException argumentException:
                            return ErrorBuilder.FromError(error)
                                .SetMessage(argumentException.Message)
                                .SetCode("ArgumentException")
                                .RemoveException()
                                .ClearExtensions()
                                .ClearLocations()
                                .Build();
                        case HotChocolate.SchemaException schemaException:
                            return ErrorBuilder.FromError(error)
                                .SetMessage(schemaException.Message)
                                .SetCode("Schema exception")
                                .Build();
                        case HotChocolate.GraphQLException graphQLException:
                            return ErrorBuilder.FromError(error)
                                .SetMessage(graphQLException.Message)
                                .SetCode("Schema exception")
                                .Build();
                    }

                    return error;
                });

                // .AddProjections()
                // .ModifyOptions(options => options.DefaultResolverStrategy = HotChocolate.Execution.ExecutionStrategy.Serial)
        }
    }
}
