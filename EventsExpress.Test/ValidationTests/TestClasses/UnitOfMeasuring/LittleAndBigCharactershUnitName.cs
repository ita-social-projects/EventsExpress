using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class LittleAndBigCharactershUnitName : IEnumerable
    {
        private readonly UnitOfMeasuringViewModel modelLitteLengthCharactersUnitName = new UnitOfMeasuringViewModel
        { Id = Guid.NewGuid(), UnitName = "888", ShortName = "rndSN" };

        private readonly UnitOfMeasuringViewModel modelBigLengthCharactersUnitName = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "888 888888888888888888888888888888888888888" +
                                                               "88888888888888888888888888888888888888888 8" +
                                                               "88888888888888888888",
            ShortName = "rndSN",
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelLitteLengthCharactersUnitName };
            yield return new object[] { modelBigLengthCharactersUnitName };
        }
    }
}
