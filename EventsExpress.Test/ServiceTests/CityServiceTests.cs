using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CityServiceTests : TestInitializer
    {
        private const string GuidId = "62FA647C-AD54-4BCC-A860-E5A2664B019D";
        private CityService cityService;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            cityService = new CityService(MockUnitOfWork.Object);
        }

        [Test]
        public void Delete_ExistingId_Success()
        {
            MockUnitOfWork.Setup(u => u.CityRepository.Get(new Guid(GuidId)))
                .Returns(new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) });

            var res = cityService.DeleteCityAsync(new Guid(GuidId));

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.CityRepository.Get(new Guid(GuidId)));

            var res = cityService.DeleteCityAsync(default);
            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Insert_NewObject_ReturnsTrue()
        {
            City city = new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) };

            MockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId))
                .Returns(new Country() { Id = new Guid(GuidId), Name = "Country1" });
            MockUnitOfWork.Setup(p => p.CityRepository.Get(string.Empty))
                .Returns(new List<City>()
               {
                   new City { Id = new Guid(GuidId), Name = "NameIsExist", CountryId = new Guid(GuidId) },
               }
               .AsQueryable());
            MockUnitOfWork.Setup(c => c.CityRepository.Insert(city));

            var res = cityService.CreateCityAsync(city);

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Insert_EmptyObject_ReturnsFalse()
        {
            City city = new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) };

            MockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid(GuidId), Name = "Country1" });

            MockUnitOfWork.Setup(p => p.CityRepository.Get(string.Empty)).Returns(new List<City>()
               {
                   new City { Id = new Guid(GuidId), Name = "Name", CountryId = new Guid(GuidId) },
               }
               .AsQueryable());
            MockUnitOfWork.Setup(c => c.CityRepository.Insert(city));

            var res = cityService.CreateCityAsync(new City());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void InsertExisting_Object_ReturnsFalse()
        {
            City city = new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) };

            MockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid(GuidId), Name = "Country1" });

            MockUnitOfWork.Setup(p => p.CityRepository.Get(string.Empty)).Returns(new List<City>()
               {
                   new City
                   {
                       Id = new Guid(GuidId),
                       Name = "City1",
                       CountryId = new Guid(GuidId),
                   },
               }
               .AsQueryable());
            MockUnitOfWork.Setup(c => c.CityRepository.Insert(city));

            var res = cityService.CreateCityAsync(city);

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Update_EmptyCity_True()
        {
            City city = new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) };

            MockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid(GuidId), Name = "Country1" });
            MockUnitOfWork.Setup(c => c.CityRepository.Get(city.Id)).Returns(new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) });

            var res = cityService.EditCityAsync(new City() { Id = new Guid(GuidId), Name = "City", CountryId = new Guid(GuidId) });

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Update_OldCityIdNull_false()
        {
            City city = new City() { Name = "City1", CountryId = new Guid(GuidId) };

            MockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid(GuidId), Name = "Country1" });
            MockUnitOfWork.Setup(c => c.CityRepository.Get(city.Id)).Returns(new City() { Id = new Guid(GuidId), Name = "City1", CountryId = new Guid(GuidId) });

            var res = cityService.EditCityAsync(new City() { Id = new Guid(GuidId), Name = "City", CountryId = new Guid(GuidId) });

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Update_CityCountryIdNull_false()
        {
            City city = new City() { Id = default, Name = "City1", CountryId = new Guid(GuidId) };

            MockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid(GuidId), Name = "Country1" });
            MockUnitOfWork.Setup(c => c.CityRepository.Get(new Guid(GuidId))).Returns(new City() { Id = default, Name = "City1", CountryId = new Guid(GuidId) });

            var res = cityService.EditCityAsync(new City() { Id = default });

            Assert.IsFalse(res.Result.Successed);
        }
    }
}
