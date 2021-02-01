namespace EventsExpress.Core.Infrastructure
{
    public class EmailOptionsModel
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public bool UseDefaultCredentials { get; set; }
    }
}
