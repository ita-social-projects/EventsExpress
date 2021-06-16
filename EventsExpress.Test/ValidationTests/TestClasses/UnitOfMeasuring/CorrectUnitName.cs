using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
   public class CorrectUnitName : IEnumerable
    {
        private readonly UnitOfMeasuringCreateViewModel modelCorrectUNWord = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "UnitName",
            ShortName = "rndSN",
        };

        private readonly UnitOfMeasuringCreateViewModel modelCorrectUNBackpaceAvg = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "Unit Name",
            ShortName = "rndSN",
        };

        private readonly UnitOfMeasuringCreateViewModel modelCorrectUNBackpaces = new UnitOfMeasuringCreateViewModel
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
