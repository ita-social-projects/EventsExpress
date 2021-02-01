using System;
using System.Runtime.Serialization;

namespace EventsExpress.Core.Exceptions
{
    [Serializable]
    public class EventsExpressException : Exception, ISerializable
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
    }
}
