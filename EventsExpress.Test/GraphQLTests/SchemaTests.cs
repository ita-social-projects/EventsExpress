using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.SortInputTypes;
using EventsExpress.Core.GraphQL.Types;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace EventsExpress.Test.GraphQLTests
{
    public class SchemaTests
    {
        [Test]
        public async Task EventSchemaChanged()
        {
            // act
            ISchema schema = await new ServiceCollection()
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
