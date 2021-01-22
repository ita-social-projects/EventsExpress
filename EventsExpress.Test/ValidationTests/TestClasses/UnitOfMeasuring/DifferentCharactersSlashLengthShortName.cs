using System;
using System.Collections;
using EventsExpress.ViewModels;

namespace EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring
{
    public class DifferentCharactersSlashLengthShortName : IEnumerable
    {
        UnitOfMeasuringViewModel modelDifferentCharactersLengthShortName = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = "7*lj9vhjvfc7",
        };

        UnitOfMeasuringViewModel modelManySlashLengthShortName = new UnitOfMeasuringViewModel
        {
            Id = Guid.NewGuid(),
            UnitName = "rndUN",
            ShortName = "7/h/gv////kjjj",
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelDifferentCharactersLengthShortName };
            yield return new object[] { modelManySlashLengthShortName };
        }
    }
}
