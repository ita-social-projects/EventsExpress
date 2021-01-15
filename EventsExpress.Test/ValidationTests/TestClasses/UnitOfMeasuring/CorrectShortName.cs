using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class CorrectShortName : IEnumerable
    {
        UnitOfMeasuringViewModel modelCorrectSNWord = new UnitOfMeasuringViewModel
        { Id = Guid.NewGuid(), UnitName = "rndUN", ShortName = "SN" };

        UnitOfMeasuringViewModel modelCorrectSNSlash = new UnitOfMeasuringViewModel
        { Id = Guid.NewGuid(), UnitName = "rndUN", ShortName = "S/N" };

        UnitOfMeasuringViewModel modelCorrectSNMoreLetterSlash = new UnitOfMeasuringViewModel
        { Id = Guid.NewGuid(), UnitName = "rndUN", ShortName = "Un/KO" };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelCorrectSNWord };
            yield return new object[] { modelCorrectSNSlash };
            yield return new object[] { modelCorrectSNMoreLetterSlash };
        }
    }
}
