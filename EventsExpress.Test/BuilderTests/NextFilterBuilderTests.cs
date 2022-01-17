using System.Linq;
using System.Reflection;
using EventsExpress.Core.Builders;
using NUnit.Framework;

namespace EventsExpress.Test.BuilderTests
{
    [TestFixture]
    public class NextFilterBuilderTests
    {
        private NextFilterBuilder<int> _builder;

        [SetUp]
        public void Initialize()
        {
            var queryable = Enumerable.Range(1, 10).AsQueryable();
            _builder = new NextFilterBuilder<int>(queryable);
        }

        [Test]
        public void Then_Always_ReturnsFilterBuilderWithSameQueryable()
        {
            var expectedQueryable = GetQueryable(_builder);

            var filterBuilder = _builder.Then();
            var actualQueryable = GetQueryable(filterBuilder);

            Assert.That(actualQueryable, Is.EqualTo(expectedQueryable));
        }

        [Test]
        public void Apply_Always_ReturnsCurrentQueryable()
        {
            var expectedQueryable = GetQueryable(_builder);

            var actualQueryable = _builder.Apply();

            Assert.That(actualQueryable, Is.EqualTo(expectedQueryable));
        }

        private IQueryable<int> GetQueryable<TBuilder>(TBuilder builder)
        {
            const string fieldName = "_queryable";
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            return (IQueryable<int>)builder
                .GetType()
                .GetField(fieldName, flags)
                ?.GetValue(builder);
        }
    }
}
