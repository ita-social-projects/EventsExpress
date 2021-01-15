using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class EmptyORManyLettersShortName : IEnumerable
    {
        UnitOfMeasuringViewModel modelEmptyShortName = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = string.Empty,
        };

        UnitOfMeasuringViewModel modelManyLettersShortName = new UnitOfMeasuringViewModel
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
