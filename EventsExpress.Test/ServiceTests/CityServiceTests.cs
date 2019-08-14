using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework.Internal;
using System.Threading.Tasks;
using EventsExpress.DTO;
using System.Linq;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    class CityServiceTests : TestInitializer
    {
        private CityService cityService;
        private City city;
        private Country country;
        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            cityService = new CityService(mockUnitOfWork.Object);
            city = new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName" };
            country = new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName" };
        }

        [Test]
        public void Delete_ExistingId_Success()
        {
            mockUnitOfWork.Setup(u => u.CityRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")))
                .Returns(new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") });


            var res = cityService.DeleteCityAsync(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"));
            Assert.IsTrue(res.Result.Successed);
        }
        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.CityRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));

            var res = cityService.DeleteCityAsync(new Guid());
            Assert.IsFalse(res.Result.Successed);
        }
        [Test]
        public void Insert_NewObject_ReturnsTrue()
        {
           
            City city = new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };
            
            mockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });

            mockUnitOfWork.Setup(p => p.CityRepository.Get("")).Returns(new List<City>()
               {
                   new City { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "NameIsExist", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") }
               }
               .AsQueryable());
            mockUnitOfWork.Setup(c => c.CityRepository.Insert(city));
       
            var res = cityService.CreateCityAsync(city);

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Insert_EmptyObject_ReturnsFalse()
        {
            City city = new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };

            mockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });

            mockUnitOfWork.Setup(p => p.CityRepository.Get("")).Returns(new List<City>()
               {
                   new City { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Name", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") }
               }
               .AsQueryable());
            mockUnitOfWork.Setup(c => c.CityRepository.Insert(city));

            var res = cityService.CreateCityAsync(new City());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void InsertExisting_Object_ReturnsFalse()
        {
            City city = new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };

            mockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });

            mockUnitOfWork.Setup(p => p.CityRepository.Get("")).Returns(new List<City>()
               {
                   new City {
                       Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
                       Name = "City1",
                       CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")
                   }
               } 
               .AsQueryable());
            mockUnitOfWork.Setup(c => c.CityRepository.Insert(city));

            var res = cityService.CreateCityAsync(city);

            Assert.IsFalse(res.Result.Successed);
        }
        [Test]
        public void Update_EmptyCity_True()
        {
            City city = new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };

            mockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });
            mockUnitOfWork.Setup(c => c.CityRepository.Get(city.Id)).Returns(new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") });

            var res = cityService.EditCityAsync(new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") });

            Assert.IsTrue(res.Result.Successed);
        }
        [Test]
        public void Update_OldCityIdNull_false()
        {
            City city = new City() {  Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };

            mockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });
            mockUnitOfWork.Setup(c => c.CityRepository.Get(city.Id)).Returns(new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") });

            var res = cityService.EditCityAsync(new City() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") });

            Assert.IsFalse(res.Result.Successed);
        }
        [Test]
        public void Update_CityCounttiIdNull_false()
        {
            City city = new City() { Id= new Guid(),  Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") };

            mockUnitOfWork.Setup(c => c.CountryRepository.Get(city.CountryId)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });
            mockUnitOfWork.Setup(c => c.CityRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"))).Returns(new City() { Id = new Guid(), Name = "City1", CountryId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D") });

            var res = cityService.EditCityAsync(new City() { Id = new Guid()});

            Assert.IsFalse(res.Result.Successed);
        }
        
    }
}