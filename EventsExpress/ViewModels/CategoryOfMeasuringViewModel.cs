using System;
using System.Collections.Generic;

namespace EventsExpress.ViewModels
{
    public class CategoryOfMeasuringViewModel
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<UnitOfMeasuringViewModel> UnitOfMeasuring { get; set; }
    }
}
