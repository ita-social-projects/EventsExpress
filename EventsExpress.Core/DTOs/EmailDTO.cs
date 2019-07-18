using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class EmailDTO
    {
        public int EmailRecipientId { get; set; }
        public string RecepientEmail { get; set; }
        public string SenderEmail { get; set; }
        public string MessageText { get; set; }
    }
}
