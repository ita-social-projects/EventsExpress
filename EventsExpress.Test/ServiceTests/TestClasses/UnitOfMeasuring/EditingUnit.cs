using System;
using System.Collections;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class EditingUnit : IEnumerable
    {
        public static UnitOfMeasuringDTO UnitOfMeasuringDTONotExId = new UnitOfMeasuringDTO
        {
            Id = Guid.NewGuid(),
            UnitName = "CorrectUnitName",
            ShortName = "CSN",
            IsDeleted = false,
        };

        public static UnitOfMeasuringDTO DeletedUnitOfMeasuringDTO = new UnitOfMeasuringDTO
        {
            Id = Guid.NewGuid(),
            UnitName = "DeletedUnitName",
            ShortName = "DSN",
            IsDeleted = true,
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { UnitOfMeasuringDTONotExId };
            yield return new object[] { DeletedUnitOfMeasuringDTO };
        }
    }
}
