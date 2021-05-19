namespace EventsExpress.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FrontConfigsViewModel
    {
        public string FACEBOOK_CLIENT_ID { get; set; }

        public string GOOGLE_CLIENT_ID { get; set; }

        public string TWITTER_CALLBACK_URL { get; set; }

        public string TWITTER_CONSUMER_KEY { get; set; }

        public string TWITTER_CONSUMER_SECRET { get; set; }
    }
}
