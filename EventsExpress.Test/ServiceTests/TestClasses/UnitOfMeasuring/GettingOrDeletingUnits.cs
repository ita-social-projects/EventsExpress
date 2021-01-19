using System;
using System.Collections;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class GettingOkDeletingUnits : IEnumerable
    {
        private Guid deletedUnitOfMeasuringId = EditingUnit.DeletedUnitOfMeasuringDTO.Id;
        private Guid notExistedId = EditingUnit.UnitOfMeasuringDTONotExId.Id;

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { notExistedId };
            yield return new object[] { deletedUnitOfMeasuringId };
        }
    }
}
