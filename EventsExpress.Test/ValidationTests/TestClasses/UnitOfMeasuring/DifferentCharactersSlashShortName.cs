using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class DifferentCharactersSlashShortName : IEnumerable
    {
        private readonly UnitOfMeasuringViewModel modelDifferentCharactersShortName = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = "7*lj",
        };

        private readonly UnitOfMeasuringViewModel modelManySlashShortName = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = "7/h/",
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelDifferentCharactersShortName };
            yield return new object[] { modelManySlashShortName };
        }
    }
}
