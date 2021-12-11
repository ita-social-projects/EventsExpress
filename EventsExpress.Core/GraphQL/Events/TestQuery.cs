namespace EventsExpress.Core.GraphQL.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Entities;
    using HotChocolate.Types;

    public class TestQuery
    {
        public IQueryable<User> GetUsers(AppDbContext context) => context.Users;
    }
}
