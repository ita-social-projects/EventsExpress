using System;
using System.Linq;
using System.Linq.Expressions;

namespace EventsExpress.Core.Builders
{
    public class FilterBuilder<T>
    {
        private bool _filterWillApply = true;
        private IQueryable<T> _queryable;

        public FilterBuilder(IQueryable<T> queryable)
        {
            _queryable = queryable;
        }

        public FilterBuilder<T> If(bool condition)
        {
            _filterWillApply = _filterWillApply && condition;
            return this;
        }

        public FilterBuilder<T> IfNotNull<TValue>(TValue value)
        {
            return If(value is not null);
        }

        public FilterBuilder<T> IfNotNull<TValue>(params TValue[] values)
        {
            foreach (var value in values)
            {
                IfNotNull(value);
            }

            return this;
        }

        public NextFilterBuilder<T> AddFilter(Expression<Func<T, bool>> filter)
        {
            _queryable = _filterWillApply ? _queryable.Where(filter) : _queryable;
            _filterWillApply = true;
            return new NextFilterBuilder<T>(_queryable);
        }
    }
}
