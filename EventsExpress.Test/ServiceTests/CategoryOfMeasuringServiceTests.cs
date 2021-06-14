using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    internal class CategoryOfMeasuringServiceTests : TestInitializer
    {
        private CategoryOfMeasuringService service;
        private CategoryOfMeasuring _categoriesOfMeasurings;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryOfMeasuringService(Context, MockMapper.Object);
            _categoriesOfMeasurings = new CategoryOfMeasuring
            {
                Id = Guid.NewGuid(),
                CategoryName = "testName",
            };

            MockMapper
               .Setup(m => m.Map<CategoryOfMeasuringDto>(It.IsAny<CategoryOfMeasuring>()))
               .Returns(new CategoryOfMeasuringDto
               {
                   Id = _categoriesOfMeasurings.Id,
                   CategoryName = "test",
               });

            Context.CategoriesOfMeasurings.Add(_categoriesOfMeasurings);
            Context.SaveChanges();
        }

        [Test]
        [Category("Get All Categories")]
        public void GetAll_ReturnsIEnumerable()
        {
            var result = service.GetAllCategories();
            Assert.IsInstanceOf<IEnumerable<CategoryOfMeasuringDto>>(result);
        }

        [Test]
        [Category("Get All Categories")]
        public void GetAll_Success()
        {
            var count = Context.CategoriesOfMeasurings.Count();
            MockMapper.Setup(u => u.Map<IEnumerable<CategoryOfMeasuringDto>>(It.IsAny<IEnumerable<CategoryOfMeasuring>>()))
                .Returns((IEnumerable<CategoryOfMeasuring> e) => e.Select(item => new CategoryOfMeasuringDto { Id = item.Id }));
            service.GetAllCategories();
            Assert.AreEqual(count, 1);
        }
    }
}
