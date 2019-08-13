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
            // mockUnitOfWork.Setup(c => c.CityRepository.Filter(filter: x=> x.CountryId == country.Id ));
            Guid guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
          //  mockUnitOfWork.Setup(c => c.CityRepository.Filter(filter: x => x.CountryId == guid));
            mockUnitOfWork.Setup(c => c.CityRepository.Insert(city));
            mockUnitOfWork.Setup(u => u.SaveAsync());
            var res = cityService.CreateCityAsync(city);

            Assert.IsTrue(res.Result.Successed);
        }
    }
}