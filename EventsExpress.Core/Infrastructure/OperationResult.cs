namespace EventsExpress.Core.Infrastructure
{
    public class OperationResult
    {
        public OperationResult(bool successed, string message, string prop)
        {
            Successed = successed;
            Message = message;
            Property = prop;
        }

        public OperationResult(bool successed)
            : this(successed, string.Empty, string.Empty)
        {
        }

        public bool Successed { get; set; }

        public string Message { get; set; }

        public string Property { get; set; }
    }
}
