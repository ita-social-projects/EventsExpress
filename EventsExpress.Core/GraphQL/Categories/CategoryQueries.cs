using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Categories
{
    [ExtendObjectType("Query")]
    public class CategoryQueries
    {
        [UseApplicationDbContext]
        public Task<List<Category>> GetCategories([ScopedService] AppDbContext context) => context.Categories.ToListAsync();
    }
}
