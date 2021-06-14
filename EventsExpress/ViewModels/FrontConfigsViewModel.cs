namespace EventsExpress.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FrontConfigsViewModel
    {
        public string FacebookClientId { get; set; }

        public string GoogleClientId { get; set; }

        public string TwitterCallbackUrl { get; set; }

        public string TwitterConsumerKey { get; set; }

        public string TwitterConsumerSecret { get; set; }

        public bool TwitterLoginEnabled { get; set; }
    }
}
