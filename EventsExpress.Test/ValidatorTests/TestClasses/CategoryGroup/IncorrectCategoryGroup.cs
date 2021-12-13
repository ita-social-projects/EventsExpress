namespace EventsExpress.Test.ValidatorTests.TestClasses.CategoryGroup
{
    using System;
    using System.Collections;
    using EventsExpress.ViewModels;

    internal class IncorrectCategoryGroup : IEnumerable
    {
        public static readonly CategoryGroupViewModel[] Groups = new CategoryGroupViewModel[3]
        {
            new CategoryGroupViewModel
            {
                Id = Guid.Empty,
                Title = string.Empty,
            },
            new CategoryGroupViewModel
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                Title = "Some Corrupted Group",
            },
            new CategoryGroupViewModel
            {
                Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                Title = "Some Group With No Purpose",
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
