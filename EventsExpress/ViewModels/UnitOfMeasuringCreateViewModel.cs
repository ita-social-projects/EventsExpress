using System;

namespace EventsExpress.ViewModels
{
    public class UnitOfMeasuringCreateViewModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string UnitName { get; set; }

        public string ShortName { get; set; }
    }
}
