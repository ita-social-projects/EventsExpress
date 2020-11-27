using System;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CountryServiceTests : TestInitializer
    {
        private Country country;
        private Guid countryId = Guid.NewGuid();
        private CountryService countryService;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            countryService = new CountryService(Context);
            country = new Country()
            {
                Id = countryId,
                Name = "Country",
            };

            Context.Countries.Add(country);
            Context.SaveChanges();
        }

        [Test]
        public void Delete_ExistingId_Success()
        {
            var res = countryService.DeleteAsync(countryId);
            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            var res = countryService.DeleteAsync(default);
            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Insert_NewObject_ReturnsTrue()
        {
            var country = new Country()
            {
                Id = Guid.NewGuid(),
                Name = "RandomName",
            };

            var res = countryService.CreateCountryAsync(country);

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Update_OldCountryIdNull_false()
        {
            var res = countryService.EditCountryAsync(new Country());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Update_Object_true()
        {
            var res = countryService.EditCountryAsync(country);

            Assert.IsTrue(res.Result.Successed);
        }
    }
}
