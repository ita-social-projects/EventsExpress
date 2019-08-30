using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class ProfileDTO
    {
        public Guid Id;
        public string Name;
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public bool IsBlocked { get; set; }
        public byte Attitude { get; set; }
        public string UserPhoto { get; set; }

        public double Rating { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; }

        public IEnumerable<Rate> MyRates { get; set; }

    }
}
