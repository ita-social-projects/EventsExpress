using System.Linq;
using System.Reflection;
using EventsExpress.Core.Builders;
using NUnit.Framework;

namespace EventsExpress.Test.BuilderTests
{
    [TestFixture]
    public class FilterBuilderTests
    {
        private FilterBuilder<int> _builder;
        private FieldInfo _conditionField;
        private FieldInfo _queryableField;

        private bool FilterWillApply
        {
            get => (bool)_conditionField.GetValue(_builder);
            set => _conditionField.SetValue(_builder, value);
        }

        private IQueryable<int> Queryable
        {
            get => (IQueryable<int>)_queryableField.GetValue(_builder);
            set => _queryableField.SetValue(_builder, value);
        }

        [SetUp]
        public void Initialize()
        {
            var queryable = Enumerable.Range(1, 10).AsQueryable();
            _builder = new FilterBuilder<int>(queryable);

            var builderType = _builder.GetType();
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            _conditionField = builderType.GetField("_filterWillApply", flags);
            _queryableField = builderType.GetField("_queryable", flags);
        }

        [Test]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        public void If_WithGivenCondition_CorrectlySetsWhetherFilterWillApply(
            bool filterWillApply,
            bool condition,
            bool expectedValue)
        {
            FilterWillApply = filterWillApply;

            _builder = _builder.If(condition);

            Assert.That(FilterWillApply, Is.EqualTo(expectedValue));
        }

        [Test]
        public void IfNotNull_WithOneParameterThatIsNull_FilterWillNotApply()
        {
            int? value = null;
            FilterWillApply = true;

            _builder = _builder.IfNotNull(value);

            Assert.That(FilterWillApply, Is.False);
        }

        [Test]
        public void IfNotNull_WithMultipleParametersThatContainNull_FilterWillNotApply()
        {
            int?[] values = { 0, null, 1 };
            FilterWillApply = true;

            _builder = _builder.IfNotNull(values);

            Assert.That(FilterWillApply, Is.False);
        }

        [Test]
        public void AddFilter_ShouldApply_SequenceIsFiltered()
        {
            const int expectedCount = 5;
            FilterWillApply = true;

            _builder.AddFilter(value => value > 5);
            int actualCount = Queryable.Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public void AddFilter_ShouldNotApply_SequenceIsNotFiltered()
        {
            const int expectedCount = 10;
            FilterWillApply = false;

            _builder.AddFilter(value => value > 5);
            int actualCount = Queryable.Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}
