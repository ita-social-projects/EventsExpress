using System;
using System.Collections.Generic;

namespace EventsExpress.Core.DTOs
{
    public class CategoryOfMeasuringDto
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<UnitOfMeasuringDto> UnitOfMeasuring { get; set; }
    }
}
