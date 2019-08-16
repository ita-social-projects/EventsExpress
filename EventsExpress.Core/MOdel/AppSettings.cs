using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.MOdel
{
    public class AppSettings
    {
        public static AppSettings appSettings { get; set; }
        public string JWTSecretKey { get; set; } 
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string JwtEmailEncryption { get; set; }
    }
}
