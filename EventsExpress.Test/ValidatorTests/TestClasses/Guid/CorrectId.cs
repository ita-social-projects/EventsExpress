namespace EventsExpress.Test.ValidatorTests.TestClasses.Guid
{
    using System;
    using System.Collections;

    public class CorrectId : IEnumerable
    {
        public static readonly Guid[] Ids = new Guid[3]
        {
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Guid.Parse("33333333-3333-3333-3333-333333333333"),
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
