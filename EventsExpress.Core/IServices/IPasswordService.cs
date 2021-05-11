using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.IServices
{
    public interface IPasswordService
    {
        string GenerateHash(string password, string salt);

        string GenerateSalt();
    }
}
