using System;
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
            var res = cityService.DeleteCityAsync(cityId);

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            var res = cityService.DeleteCityAsync(default);
            Assert.IsFalse(res.Result.Successed);
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

            var res = cityService.CreateCityAsync(city);

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Insert_EmptyObject_ReturnsFalse()
        {
            var res = cityService.CreateCityAsync(new City());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void InsertExisting_Object_ReturnsFalse()
        {
            var res = cityService.CreateCityAsync(city);

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Update_EmptyCity_True()
        {
            var res = cityService.EditCityAsync(new City()
            {
                Id = cityId,
                Name = "City1",
                CountryId = countryId,
            });

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Update_OldCityIdNull_false()
        {
            var res = cityService.EditCityAsync(new City()
            {
                Id = Guid.Empty,
                Name = "City",
                CountryId = countryId,
            });

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Update_CityCountryIdNull_false()
        {
            var res = cityService.EditCityAsync(new City() { Id = default });

            Assert.IsFalse(res.Result.Successed);
        }
    }
}
