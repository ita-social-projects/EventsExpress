using EventsExpress.Core.GraphQL.SortInputTypes;
using EventsExpress.Core.GraphQL.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Data.Sorting;
using HotChocolate.Types.Descriptors;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.GraphQL.Extensions
{
    public static class GraphQLServiceExtension
    {
        public static void AddGraphQLService(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                    .AddQueryType<QueryType>()

                .AddType<EventType>()
                .AddType<CategoryType>()
                .AddType<EventLocationType>()
                .AddType<UserType>()
                .AddType<PointSortType>()

                .AddProjections()
                .AddSpatialTypes()
                .AddSpatialProjections()
                .AddFiltering()
                .AddSpatialFiltering()
                .AddSorting();
        }
    }
}
