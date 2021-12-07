using System.Security.Cryptography;
using System.Text;
using EventsExpress.Db.Bridge;

namespace EventsExpress.Core.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string GenerateHash(string password, string salt)
        {
            byte[] byteSourceText = Encoding.ASCII.GetBytes(salt + password);
            using var hashProvider = SHA256.Create();
            byte[] byteHash = hashProvider.ComputeHash(byteSourceText);

            return Encoding.ASCII.GetString(byteHash);
        }

        public string GenerateSalt()
        {
            using var provider = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            provider.GetBytes(salt);
            return Encoding.ASCII.GetString(salt);
        }
    }
}
