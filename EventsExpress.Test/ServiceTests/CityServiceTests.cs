using System;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CityServiceTests : TestInitializer
    {
        private Guid cityId = Guid.NewGuid();
        private Guid countryId = Guid.NewGuid();
        private CityService cityService;
        private City city;
        private Country country;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            cityService = new CityService(Context);

            country = new Country()
            {
                Id = countryId,
                Name = "Country",
            };

            city = new City()
            {
                Id = cityId,
                Name = "City",
                Country = country,
            };

            Context.Countries.Add(country);
            Context.Cities.Add(city);
            Context.SaveChanges();
        }

        [Test]
        public void Delete_ExistingId_Success()
        {
            Assert.DoesNotThrowAsync(async () => await cityService.DeleteCityAsync(cityId));
        }

        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await cityService.DeleteCityAsync(default));
        }

        [Test]
        public void Insert_NewObject_ReturnsTrue()
        {
            var city = new City()
            {
                Name = "City1",
                CountryId = countryId,
                Country = country,
            };

            Assert.DoesNotThrowAsync(async () => await cityService.CreateCityAsync(city));
        }

        [Test]
        public void Insert_EmptyObject_ReturnsFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await cityService.CreateCityAsync(new City()));
        }

        [Test]
        public void InsertExisting_Object_ReturnsFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await cityService.CreateCityAsync(city));
        }

        [Test]
        public void Update_EmptyCity_True()
        {
            Assert.DoesNotThrowAsync(async () => await cityService.EditCityAsync(new City()
            {
                Id = cityId,
                Name = "City1",
                CountryId = countryId,
            }));
        }

        [Test]
        public void Update_OldCityIdNull_false()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await cityService.EditCityAsync(new City()
            {
                Id = Guid.Empty,
                Name = "City",
                CountryId = countryId,
            }));
        }

        [Test]
        public void Update_CityCountryIdNull_false()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await cityService.EditCityAsync(new City() { Id = default }));
        }
    }
}
