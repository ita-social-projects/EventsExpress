using System.Linq;
using EventsExpress.Core.Builders;
using NUnit.Framework;

namespace EventsExpress.Test.BuilderTests
{
    [TestFixture]
    public class FilterBuilderTests
    {
        private FilterBuilder<int> _builder;
        private IQueryable<int> _queryable;

        [SetUp]
        protected void Initialize()
        {
            _queryable = Enumerable.Range(1, 10).AsQueryable();
            _builder = new FilterBuilder<int>(_queryable);
        }

        [Test]
        public void AddFilter_ConditionIsTrue_FilterIsApplied()
        {
            var expectedElements = _queryable.Where(value => value > 5).ToList();
            var valueForCondition = new object();

            var actualElements = _builder
                .IfNotNull(valueForCondition)
                .AddFilter(value => value > 5)
                .Apply()
                .ToList();

            Assert.That(actualElements, Is.EquivalentTo(expectedElements));
        }

        [Test]
        public void AddFilter_ConditionIsFalse_FilterIsNotApplied()
        {
            var expectedElements = _queryable.ToList();
            object[] valuesForCondition = { new object(), null, "text" };

            var actualElements = _builder
                .IfNotNull(valuesForCondition)
                .AddFilter(value => value > 5)
                .Apply()
                .ToList();

            Assert.That(actualElements, Is.EquivalentTo(expectedElements));
        }

        [Test]
        public void AddFilter_MultipleFiltersWithDifferentConditions_CorrectFiltersAreApplied()
        {
            var expectedElements = _queryable
                .Where(value => value > 5 && value % 2 == 0)
                .ToList();

            var actualElements = _builder
                    .AddFilter(value => value % 2 == 0)
                .Then()
                    .If(false)
                    .AddFilter(value => value < 9)
                .Then()
                    .If(true)
                    .AddFilter(value => value > 5)
                .Apply()
                .ToList();

            Assert.That(actualElements, Is.EquivalentTo(expectedElements));
        }
    }
}
