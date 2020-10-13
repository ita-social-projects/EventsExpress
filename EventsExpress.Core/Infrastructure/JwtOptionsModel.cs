using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace EventsExpress.Core.Infrastructure
{
   public class JwtOptionsModel
    {
        public string SecretKey { get; set; }
        public double LifeTime { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string JwtEmailEncryption { get; set; }

    }
}
