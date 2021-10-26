﻿namespace EventsExpress.Core.Infrastructure
{
    public class JwtOptionsModel
    {
        public string SecretKey { get; set; }

        public virtual double LifeTime { get; set; }

        public string GoogleClientId { get; set; }

        public string GoogleClientSecret { get; set; }
    }
}
