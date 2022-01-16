using System.Linq;
using EventsExpress.Core.Builders;

namespace EventsExpress.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static FilterBuilder<T> Filters<T>(this IQueryable<T> queryable)
        {
            return new FilterBuilder<T>(queryable);
        }
    }
}
