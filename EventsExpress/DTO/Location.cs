using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class Location
    {
        public Guid CityId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
