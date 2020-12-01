using System;
using EventsExpress.Core.Exceptions;
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
            Assert.DoesNotThrowAsync(async () => await countryService.DeleteAsync(countryId));
        }

        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await countryService.DeleteAsync(default));
        }

        [Test]
        public void Insert_NewObject_ReturnsTrue()
        {
            var country = new Country()
            {
                Id = Guid.NewGuid(),
                Name = "RandomName",
            };

            Assert.DoesNotThrowAsync(async () => await countryService.CreateCountryAsync(country));
        }

        [Test]
        public void Update_OldCountryIdNull_false()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await countryService.EditCountryAsync(new Country()));
        }

        [Test]
        public void Update_Object_true()
        {
            Assert.DoesNotThrowAsync(async () => await countryService.EditCountryAsync(country));
        }
    }
}
