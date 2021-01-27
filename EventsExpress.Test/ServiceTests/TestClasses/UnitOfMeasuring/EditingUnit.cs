using System;
using System.Collections;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class EditingUnit : IEnumerable
    {
        private static UnitOfMeasuringDTO unitOfMeasuringDTONotExId = new UnitOfMeasuringDTO
        {
            Id = Guid.NewGuid(),
            UnitName = "CorrectUnitName",
            ShortName = "CSN",
            IsDeleted = false,
        };

        private static UnitOfMeasuringDTO deletedUnitOfMeasuringDTO = new UnitOfMeasuringDTO
        {
            Id = Guid.NewGuid(),
            UnitName = "DeletedUnitName",
            ShortName = "DSN",
            IsDeleted = true,
        };

        public static UnitOfMeasuringDTO DeletedUnitOfMeasuringDTO
        {
            get
            {
                return deletedUnitOfMeasuringDTO;
            }

            set
            {
                deletedUnitOfMeasuringDTO = value;
            }
        }

        public static UnitOfMeasuringDTO UnitOfMeasuringDTONotExId
        {
            get
            {
                return unitOfMeasuringDTONotExId;
            }

            set
            {
                unitOfMeasuringDTONotExId = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { unitOfMeasuringDTONotExId };
            yield return new object[] { deletedUnitOfMeasuringDTO };
        }
    }
}
