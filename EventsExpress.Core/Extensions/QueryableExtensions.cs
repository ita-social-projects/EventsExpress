using System;
using System.Linq;
using System.Linq.Expressions;
using EventsExpress.Core.Builders;

namespace EventsExpress.Core.Extensions
{
    public static partial class QueryableExtensions
    {
        public static FilterBuilder<T> Filters<T>(this IQueryable<T> queryable)
        {
            return new FilterBuilder<T>(queryable);
        }
    }
}
