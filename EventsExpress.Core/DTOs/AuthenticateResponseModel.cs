using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class AuthenticateResponseModel
    {

        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

        public AuthenticateResponseModel(string jwtToken, string refreshToken)
        {
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }

    }
}
