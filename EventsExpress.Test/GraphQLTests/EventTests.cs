using System;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.SortInputTypes;
using EventsExpress.Core.GraphQL.Types;
using EventsExpress.Db.EF;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace EventsExpress.Test.GraphQLTests
{
    public class EventTests
    {
        [Test]
        public async Task EventSchemaChanged()
        {
            // arrange
            // act
            ISchema schema = await new ServiceCollection()
                .AddDbContextFactory<AppDbContext>(options =>
                    options.UseInMemoryDatabase("eventsTest"))

                .AddGraphQL()
                .AddQueryType<QueryType>()

                .AddType<EventType>()
                .AddType<CategoryType>()
                .AddType<UserType>()
                .AddType<PointSortType>()

                .AddProjections()
                .AddFiltering()
                .AddSorting()

                .AddSpatialProjections()
                .AddSpatialTypes()

                .BuildSchemaAsync();

            // assert
            schema.Print().MatchSnapshot();
        }
    }
}
