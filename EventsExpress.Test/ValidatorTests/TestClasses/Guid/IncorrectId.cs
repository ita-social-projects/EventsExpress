namespace EventsExpress.Test.ValidatorTests.TestClasses.Guid
{
    using System;
    using System.Collections;

    public class IncorrectId : IEnumerable
    {
        public static readonly Guid[] Ids = new Guid[3]
        {
            Guid.Empty,
            Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
        };

        public IEnumerator GetEnumerator()
        {
            foreach (Guid id in Ids)
            {
                yield return id;
            }
        }
    }
}
