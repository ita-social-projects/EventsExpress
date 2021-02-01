using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
   public class CorrectUnitName : IEnumerable
    {
        private readonly UnitOfMeasuringViewModel modelCorrectUNWord = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "UnitName",
            ShortName = "rndSN",
        };

        private readonly UnitOfMeasuringViewModel modelCorrectUNBackpaceAvg = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "Unit Name",
            ShortName = "rndSN",
        };

        private readonly UnitOfMeasuringViewModel modelCorrectUNBackpaces = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "Unit Name Klo ",
            ShortName = "rndSN",
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelCorrectUNWord };
            yield return new object[] { modelCorrectUNBackpaces };
            yield return new object[] { modelCorrectUNBackpaceAvg };
        }
    }
}
