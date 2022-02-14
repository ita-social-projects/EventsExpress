using System.Linq;
using EventsExpress.Core.Extensions;

namespace EventsExpress.Core.Builders
{
    public class NextFilterBuilder<T>
    {
        private readonly IQueryable<T> _queryable;

        public NextFilterBuilder(IQueryable<T> queryable)
        {
            _queryable = queryable;
        }

        public FilterBuilder<T> Then()
        {
            return _queryable.Filters();
        }

        public IQueryable<T> Apply()
        {
            return _queryable;
        }
    }
}
