using System.Security.Cryptography;
using System.Text;
using EventsExpress.Core.Services;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    public class PasswordHasherServiceTests : TestInitializer
    {
        private static string salt = "salt";
        private static string password = "password";

        private PasswordHasherService passwordHasherService;

        private byte[] byteHash = new byte[] { 2, 6, 8 };
        private byte[] byteText = Encoding.ASCII.GetBytes(salt + password);

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            passwordHasherService = new PasswordHasherService();
        }

        [Test]
        public void GenerateHash_IsNotNull()
        {
            var res = passwordHasherService.GenerateHash(password, salt);

            Assert.IsNotNull(res);
        }

        [Test]
        public void GenerateSalt_IsNotNull()
        {
            var res = passwordHasherService.GenerateSalt();

            Assert.IsNotNull(res);
        }
    }
}
