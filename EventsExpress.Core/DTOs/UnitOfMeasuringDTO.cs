using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class UnitOfMeasuringDto
    {
        public Guid Id { get; set; }

        public string UnitName { get; set; }

        public string ShortName { get; set; }

        public bool IsDeleted { get; set;  }
    }
}
