using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Infrastructure
{
    public class OperationResult
    {
        public bool Successed { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }

        public OperationResult(bool successed, string message, string prop)
        {
            Successed = successed;
            Message = message;
            Property = prop;
        }

        public OperationResult(bool successed) : this(successed, "", "")
        {
        }
    }
}
