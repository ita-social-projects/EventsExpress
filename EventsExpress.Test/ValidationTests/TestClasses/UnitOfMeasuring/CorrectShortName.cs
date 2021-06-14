using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class CorrectShortName : IEnumerable
    {
        private readonly UnitOfMeasuringCreateViewModel modelCorrectSNWord = new UnitOfMeasuringCreateViewModel
        { Id = Guid.NewGuid(), UnitName = "rndUN", ShortName = "SN" };

        private readonly UnitOfMeasuringCreateViewModel modelCorrectSNSlash = new UnitOfMeasuringCreateViewModel
        { Id = Guid.NewGuid(), UnitName = "rndUN", ShortName = "S/N" };

        private readonly UnitOfMeasuringCreateViewModel modelCorrectSNMoreLetterSlash = new UnitOfMeasuringCreateViewModel
        { Id = Guid.NewGuid(), UnitName = "rndUN", ShortName = "Un/KO" };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelCorrectSNWord };
            yield return new object[] { modelCorrectSNSlash };
            yield return new object[] { modelCorrectSNMoreLetterSlash };
        }
    }
}
