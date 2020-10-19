using System.Collections.Generic;

namespace EventsExpress.ViewModel
{
    public class IndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
