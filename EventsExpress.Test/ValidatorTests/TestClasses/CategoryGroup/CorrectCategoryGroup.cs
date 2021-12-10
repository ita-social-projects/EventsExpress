namespace EventsExpress.Test.ValidatorTests.TestClasses.CategoryGroup
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using EventsExpress.ViewModels;

    internal class CorrectCategoryGroup : IEnumerable
    {
        public static readonly CategoryGroupViewModel[] Groups = new CategoryGroupViewModel[3]
        {
            new CategoryGroupViewModel
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Some Group #1",
            },
            new CategoryGroupViewModel
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "Some Group #2",
            },
            new CategoryGroupViewModel
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Title = "Some Group #3",
            },
        };

        public IEnumerator GetEnumerator()
        {
            foreach (var item in Groups)
            {
                yield return item;
            }
        }
    }
}
