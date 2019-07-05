using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Infrastructure
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
