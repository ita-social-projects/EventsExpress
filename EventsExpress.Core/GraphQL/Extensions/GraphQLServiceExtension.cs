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
            services.AddScoped<IEventGraphQLService, EventGraphQLService>();

            services
                .AddGraphQLServer()
                    .AddQueryType<QueryType>()
                .AddType<EventType>()
                .AddType<CategoryType>()
                .AddType<UserType>()
                .AddType<EventLocationType>()

                .AddSpatialTypes()

                .AddProjections()
                .AddFiltering();
        }
    }
}
