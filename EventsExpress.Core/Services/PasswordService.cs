using System.Security.Cryptography;
using System.Text;
using EventsExpress.Core.IServices;

namespace EventsExpress.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPhotoService photoService;

        public PasswordService(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string GenerateHash(string password, string salt)
        {
            byte[] byteSourceText = Encoding.ASCII.GetBytes(salt + password);
            using var hashProvider = new SHA256Managed();
            byte[] byteHash = hashProvider.ComputeHash(byteSourceText);

            return Encoding.ASCII.GetString(byteHash);
        }

        public string GenerateSalt()
        {
            using var provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            provider.GetBytes(salt);

            return Encoding.ASCII.GetString(salt);
        }
    }
}
