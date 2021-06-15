using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class EmptyORManyLettersShortName : IEnumerable
    {
        private readonly UnitOfMeasuringCreateViewModel modelEmptyShortName = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = string.Empty,
        };

        private readonly UnitOfMeasuringCreateViewModel modelManyLettersShortName = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = "ShortName/has/many/Letters",
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelEmptyShortName };
            yield return new object[] { modelManyLettersShortName };
        }
    }
}
