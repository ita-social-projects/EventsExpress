using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring
{
    public class ExistingUnitByName : IEnumerable
    {
        private string correctUnitName = "CorrectUnitName";
        private string correctShortName = "CSN";
        private string correctCategory = "CorrectCategory";
        private string deletedUnitName = "DeletedUnitName";
        private string deletedShortName = "DSN";
        private string deletedCategory = "DeletedCategory";
        private string randomStringName = "Rnd";
        private string randomCategory = "RndCategory";

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { correctUnitName, correctShortName, correctCategory, Is.True };
            yield return new object[] { randomStringName, correctShortName, correctCategory, Is.False };
            yield return new object[] { correctUnitName, randomStringName, correctCategory,  Is.False };
            yield return new object[] { correctUnitName, correctShortName, randomCategory, Is.False };
            yield return new object[] { deletedUnitName, deletedShortName, deletedCategory, Is.False };
        }
    }
}
