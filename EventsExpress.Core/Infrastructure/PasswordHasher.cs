using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EventsExpress.Core.Infrastructure
{
    public static class PasswordHasher
    {    
        public static string GenerateHash(string password, string salt = "a192862aa3bf46dffb57b12bdcc4c199")
        {
            var byteSalt = Encoding.UTF8.GetBytes(salt);
            var bytePassword = Encoding.UTF8.GetBytes(password);
            var hmacMD5 = new HMACMD5(byteSalt);
            var saltedHash = hmacMD5.ComputeHash(bytePassword);

            return Encoding.UTF8.GetString(saltedHash, 0, saltedHash.Length);
        }

    }
}
