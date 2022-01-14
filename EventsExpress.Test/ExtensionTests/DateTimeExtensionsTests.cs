using System;
using EventsExpress.Core.Extensions;
using NUnit.Framework;

namespace EventsExpress.Test.ExtensionTests
{
    [TestFixture]
    internal class DateTimeExtensionsTests
    {
        [Test]
        [TestCase("2004-01-01", "2022-01-01", 18)]
        [TestCase("2022-01-01", "2004-01-01", 18)]
        [TestCase("2004-02-29", "2022-02-28", 17)]
        [TestCase("2022-02-28", "2004-02-29", 17)]
        public void GetDifferenceInYears_DatesHaveDifferentYears_ReturnsCorrectValue(string first, string second, int expectedDiff)
        {
            var firstDate = DateTime.Parse(first);
            var secondDate = DateTime.Parse(second);

            int diff = firstDate.GetDifferenceInYears(secondDate);

            Assert.That(diff, Is.EqualTo(expectedDiff));
        }

        [Test]
        [TestCase("2022-02-01", "2022-01-01")]
        [TestCase("2021-12-31", "2022-01-01")]
        [TestCase("2022-01-01", "2021-12-31")]
        [TestCase("2022-01-01", "2021-01-02")]
        [TestCase("2021-01-02", "2022-01-01")]
        public void GetDifferenceInYears_DatesAreWithinSameYear_ReturnsZero(string first, string second)
        {
            var firstDate = DateTime.Parse(first);
            var secondDate = DateTime.Parse(second);

            int diff = firstDate.GetDifferenceInYears(secondDate);

            Assert.That(diff, Is.Zero);
        }
    }
}
