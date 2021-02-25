using System;
using System.Collections.Generic;

namespace EventsExpress.Core.Exceptions
{
    public class EventsExpressException : Exception
    {
        public EventsExpressException()
        {
        }

        public EventsExpressException(string message)
            : base(message)
        {
        }

        public EventsExpressException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EventsExpressException(string message, Dictionary<string, string> customData)
            : base(message)
        {
            CustomData = customData;
        }

        public Dictionary<string, string> CustomData { get; set; }
    }
}
