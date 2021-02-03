using System.Security.Cryptography;
using System.Text;

namespace EventsExpress.Db.Helpers
{
    public static class PasswordHasher
    {
        public static string GenerateHash(string password, string salt)
        {
            byte[] byteSourceText = Encoding.ASCII.GetBytes(salt + password);
            var md5Hash = new MD5CryptoServiceProvider();
            byte[] byteHash = md5Hash.ComputeHash(byteSourceText);

            return Encoding.ASCII.GetString(byteHash);
        }

        public static string GenerateSalt()
        {
            using var provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            provider.GetBytes(salt);

            return Encoding.ASCII.GetString(salt);
        }
    }
}
