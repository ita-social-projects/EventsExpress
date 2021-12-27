using EventsExpress.Core.GraphQL.DataLoaders;
using EventsExpress.Core.GraphQL.IDataLoaders;
using EventsExpress.Core.GraphQL.IServices;
using EventsExpress.Core.GraphQL.Services;
using EventsExpress.Core.GraphQL.Types;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.GraphQL.Extensions
{
    public static class GraphQLServiceExtension
    {
        public static void AddGraphQLService(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();

            services
                .AddGraphQLServer()
                    .AddQueryType<QueryType>()
                .AddType<EventType>()
                .AddType<CategoryType>()
                .AddProjections()

                .AddDataLoader<ICategoryByIdDataLoader, CategoryByIdDataLoader>()
                ;
        }
    }
}
