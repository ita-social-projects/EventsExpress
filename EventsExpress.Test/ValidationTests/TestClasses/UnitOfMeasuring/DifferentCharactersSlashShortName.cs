using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class DifferentCharactersSlashShortName : IEnumerable
    {
        private readonly UnitOfMeasuringCreateViewModel modelDifferentCharactersShortName = new UnitOfMeasuringCreateViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = "7*lj",
        };

        private readonly UnitOfMeasuringCreateViewModel modelManySlashShortName = new UnitOfMeasuringCreateViewModel
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
