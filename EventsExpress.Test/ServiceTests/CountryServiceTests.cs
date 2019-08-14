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
    class CountryServiceTests : TestInitializer
    {
        private Country country;
        private CountryService countryService;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            countryService = new CountryService(mockUnitOfWork.Object);
            country = new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName" };
        }
        [Test]
        public void Delete_ExistingId_Success()
        {
            mockUnitOfWork.Setup(u => u.CountryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")))
                .Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "City1" });


            var res = countryService.DeleteAsync(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"));
            Assert.IsTrue(res.Result.Successed);
        }
        [Test]
        public void Delete_NotExistId_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.CountryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));

            var res = countryService.DeleteAsync(new Guid());
            Assert.IsFalse(res.Result.Successed);
        }
        [Test]
        public void Insert_NewObject_ReturnsTrue()
        {
            mockUnitOfWork.Setup(c => c.CountryRepository.Insert(country));

            var res = countryService.CreateCountryAsync(country);

            Assert.IsTrue(res.Result.Successed);
        }
        [Test]
        public void Update_OldCountryIdNull_false()
        {
            
            mockUnitOfWork.Setup(c => c.CountryRepository.Get(country.Id)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });
          
            var res = countryService.EditCountryAsync(new Country());

            Assert.IsFalse(res.Result.Successed);
        }
        [Test]
        public void Update_Object_true()
        {

            mockUnitOfWork.Setup(c => c.CountryRepository.Get(country.Id)).Returns(new Country() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "Country1" });

            var res = countryService.EditCountryAsync(country);

            Assert.IsTrue(res.Result.Successed);
        }

    }
}
