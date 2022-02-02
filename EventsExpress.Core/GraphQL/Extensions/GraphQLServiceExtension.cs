using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.SortInputTypes;
using EventsExpress.Core.GraphQL.Types;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.GraphQL.Extensions
{
    public static class GraphQLServiceExtension
    {
        public static async Task<IRequestExecutor> AddGraphQLService(this IServiceCollection services)
        {
            return await services
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
                .AddSorting()
                .BuildRequestExecutorAsync();
        }
    }
}
