using System;
using System.Collections;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class EditingUnit : IEnumerable
    {
        UnitOfMeasuringDTO unitOfMeasuringDTONotExId = new UnitOfMeasuringDTO
        {
            Id = new Guid("e815d623-6b0c-4c85-a847-26a6f5a878b6"),
            UnitName = "CorrectUnitName",
            ShortName = "CSN",
            IsDeleted = false,
        };

        UnitOfMeasuringDTO deletedUnitOfMeasuringDTO = new UnitOfMeasuringDTO
        {
            Id = new Guid("a1d2dc99-0d30-49e2-b2ee-12411dc461ff"),
            UnitName = "DeletedUnitName",
            ShortName = "DSN",
            IsDeleted = true,
        };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { unitOfMeasuringDTONotExId };
            yield return new object[] { deletedUnitOfMeasuringDTO };
        }
    }
}
