using System.Linq;
using EventsExpress.Core.Extensions;
using NUnit.Framework;

namespace EventsExpress.Test.ExtensionTests
{
    [TestFixture]
    public class QueryableExtensionsTests
    {
        [Test]
        [TestCase(2, 2, 2)]
        [TestCase(2, 15, 5)]
        [TestCase(2, 20, 0)]
        [TestCase(4, 6, 2)]
        public void Page_WithValidParameters_ReturnsCorrectElementCount(int pageNumber, int pageSize, int expectedCount)
        {
            const int start = 1;
            const int count = 20;
            var queryable = Enumerable.Range(start, count).AsQueryable();

            var page = queryable.Page(pageNumber, pageSize);
            int actualCount = page.Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase(1, 3, 1)]
        [TestCase(3, 5, 11)]
        public void Page_WithValidParameters_ReturnsCorrectElements(int pageNumber, int pageSize, int pageStart)
        {
            const int start = 1;
            const int totalCount = 20;
            var queryable = Enumerable.Range(start, totalCount).AsQueryable();
            var expectedPage = Enumerable.Range(pageStart, pageSize).ToList();

            var actualPage = queryable.Page(pageNumber, pageSize).ToList();

            Assert.That(actualPage, Is.EquivalentTo(expectedPage));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Page_WithInvalidNumber_ReturnsElementsFromStart(int pageNumber)
        {
            const int pageSize = 5;
            const int start = 1;
            const int totalCount = 20;
            var queryable = Enumerable.Range(start, totalCount).AsQueryable();
            var expectedPage = Enumerable.Range(start, pageSize).ToList();

            var actualPage = queryable.Page(pageNumber, pageSize).ToList();

            Assert.That(actualPage, Is.EquivalentTo(expectedPage));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Page_WithInvalidSize_ReturnsEmptyResult(int pageSize)
        {
            const int pageNumber = 1;
            const int start = 1;
            const int totalCount = 20;
            var queryable = Enumerable.Range(start, totalCount).AsQueryable();

            var page = queryable.Page(pageNumber, pageSize).ToList();

            Assert.That(page, Is.Empty);
        }
    }
}
