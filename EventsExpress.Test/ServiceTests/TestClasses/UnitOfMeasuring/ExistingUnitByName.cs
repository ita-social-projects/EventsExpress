using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class ExistingUnitByName : IEnumerable
    {
        private string correctUnitName = "CorrectUnitName";
        private string correctShortName = "CSN";
        private string deletedUnitName = "DeletedUnitName";
        private string deletedShortName = "DSN";
        private string randomStringName = "Rnd";

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { correctUnitName, correctShortName, Is.True };
            yield return new object[] { randomStringName, correctShortName, Is.False };
            yield return new object[] { correctUnitName, randomStringName, Is.False };
            yield return new object[] { deletedUnitName, deletedShortName, Is.False };
        }
    }
}
