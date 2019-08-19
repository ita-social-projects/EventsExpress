using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class EmailDTO
    {
        public string RecepientEmail { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }
}
