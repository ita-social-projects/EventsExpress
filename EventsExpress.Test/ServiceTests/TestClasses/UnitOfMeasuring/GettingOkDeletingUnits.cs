using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class GettingOkDeletingUnits
    {
        private static Guid deletedUnitOfMeasuringId = EditingUnit.DeletedUnitOfMeasuringDTO.Id;
        private static Guid notExistedId = EditingUnit.UnitOfMeasuringDTONotExId.Id;

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(notExistedId)
                    .SetName("Get_NotExistingId_Exception");
                yield return new TestCaseData(deletedUnitOfMeasuringId)
                    .SetName("Get_DeletedUnit_Exception");
            }
        }
    }
}
