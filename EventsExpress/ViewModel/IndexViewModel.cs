using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
