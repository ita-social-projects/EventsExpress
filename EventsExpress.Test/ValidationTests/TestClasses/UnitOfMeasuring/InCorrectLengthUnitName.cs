using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class InCorrectLengthUnitName : IEnumerable
    {
        private readonly UnitOfMeasuringCreateViewModel modelLitteLengthUnitName = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "888",
            ShortName = "rndSN",
        };

        private readonly UnitOfMeasuringCreateViewModel modelBigLengthUnitName = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "888 888888888888888888888888888888888888888" +
                                                    "88888888888888888888888888888888888888888 8" +
                                                    "88888888888888888888",
            ShortName = "rndSN",
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelBigLengthUnitName };
            yield return new object[] { modelLitteLengthUnitName };
        }
    }
}
