using System;
using System.Collections;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class GettingOkDeletingUnits : IEnumerable
    {
        private Guid deletedUnitOfMeasuringId = new Guid("a1d2dc99-0d30-49e2-b2ee-12411dc461ff");
        private Guid notExistedId = new Guid("e815d623-6b0c-4c85-a847-26a6f5a878b6");

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { notExistedId };
            yield return new object[] { deletedUnitOfMeasuringId };
        }
    }
}
