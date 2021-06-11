using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Test.ServiceTests.TestClasses.UnitOfMeasuring;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace EventsExpress.Test.ServiceTests
{
    internal class UnitOfMeasuringServiceTests : TestInitializer
    {
        private CategoryOfMeasuringService categoryOfMeasuringService;
        private UnitOfMeasuringService service;
        private UnitOfMeasuringDto correctUnitOfMeasuringDTO;
        private UnitOfMeasuring unitOfMeasuring;
        private CategoryOfMeasuring categoryOfMeasuring = new CategoryOfMeasuring
        {
            Id = Guid.NewGuid(),
            CategoryName = "testName",
        };

        private CategoryOfMeasuringDto categoryOfMeasuringDto = new CategoryOfMeasuringDto
        {
            Id = Guid.NewGuid(),
            CategoryName = "testDtoName",
        };

        private Guid id = Guid.NewGuid();
        public const string CreateUnitName = "create Unit Name";
        public const string CreateShortName = "create Unit Name";
        public const string CorrectUnitName = "CorrectUnitName";
        public const string CorrectShortName = "CSN";
        public const string CreateCategory = "create Category";
        public const string CorrectCategory = "CorrectCategory";
        public const string DeletedUnitName = "DeletedUnitName";
        public const string DeletedShortName = "DSN";
        public const string DeletedCategory = "DeleteCategory";
        public const string RandomStringName = "Rnd";
        public const string InCorrectUnitName = "78789878Unitk";

        public UnitOfMeasuring FromDTOToUnit(UnitOfMeasuringDto unitOfMeasuringDTO)
        {
            return new UnitOfMeasuring
            {
                Id = unitOfMeasuringDTO.Id,
                UnitName = unitOfMeasuringDTO.UnitName,
                ShortName = unitOfMeasuringDTO.ShortName,
                Category = categoryOfMeasuring,
                IsDeleted = unitOfMeasuringDTO.IsDeleted,
            };
        }

        public void InitTestData()
        {
            correctUnitOfMeasuringDTO = new UnitOfMeasuringDto
            {
                Id = Guid.NewGuid(),
                UnitName = CorrectUnitName,
                ShortName = CorrectShortName,
                Category = categoryOfMeasuringDto,
                IsDeleted = false,
            };
        }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            InitTestData();
            categoryOfMeasuringService = new CategoryOfMeasuringService(Context, MockMapper.Object);
            service = new UnitOfMeasuringService(Context, MockMapper.Object, categoryOfMeasuringService);
            unitOfMeasuring = new UnitOfMeasuring
            {
                UnitName = "RandomUnitName",
                ShortName = "RandomShortName",
                CategoryId = Guid.NewGuid(),
            };
            UnitOfMeasuring correctUnitOfMeasuring = FromDTOToUnit(correctUnitOfMeasuringDTO);
            Context.UnitOfMeasurings.Add(correctUnitOfMeasuring);
            Context.UnitOfMeasurings.Add(unitOfMeasuring);
            Context.SaveChanges();
            MockMapper.Setup(u => u.Map<UnitOfMeasuringDto, UnitOfMeasuring>(It.IsAny<UnitOfMeasuringDto>()))
              .Returns((UnitOfMeasuringDto e) => e == null ?
              null :
              new UnitOfMeasuring
              {
                  Id = e.Id,
                  UnitName = e.UnitName,
                  ShortName = e.ShortName,
                  Category = categoryOfMeasuring,
                  IsDeleted = e.IsDeleted,
              });

            MockMapper.Setup(u => u.Map<IEnumerable<UnitOfMeasuring>, IEnumerable<UnitOfMeasuringDto>>(It.IsAny<IEnumerable<UnitOfMeasuring>>()))
             .Returns((IEnumerable<UnitOfMeasuring> e) => e?.Select(item => new UnitOfMeasuringDto { Id = item.Id }));
        }

        [Test]
        [Category("Get All")]
        public void Get_ALL_CorrectData()
        {
            var res = service.GetAll();
            Assert.That(res.Any(item => item.Id == correctUnitOfMeasuringDTO.Id), Is.True);
        }

        [Test]
        [Category("Create")]
        public void Create_NuLLDTO_Exception()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(null));
        }

        [Test]
        [Category("Create")]
        public async System.Threading.Tasks.Task Create_CorrectDTO_IdUnit()
        {
            UnitOfMeasuringDto unitOfMeasuringDTOCreate = new UnitOfMeasuringDto
            {
                Id = Guid.NewGuid(),
                UnitName = CreateUnitName,
                ShortName = CreateShortName,
                Category = categoryOfMeasuringDto,
                IsDeleted = false,
            };

            Guid unitId = await service.Create(unitOfMeasuringDTOCreate);
            Assert.That(unitId, Is.Not.Null);
        }

        [TestCaseSource(typeof(EditingUnit))]
        [Category("Edit")]
        public void Edit_NotExistingIdORDeletedUnit_Exception(UnitOfMeasuringDto unitType)
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(unitType));
        }

        [Test]
        [Category("Edit")]
        public async System.Threading.Tasks.Task Edit_ValidDTO_IdUnitOfMeasuring()
        {
            Guid expectedId = correctUnitOfMeasuringDTO.Id;
            Guid actualId = await service.Edit(correctUnitOfMeasuringDTO);
            Assert.That(actualId, Is.EqualTo(expectedId));
        }

        [Test]
        [Category("Get")]
        public void Get_ExistedUnit_Unit()
        {
            Guid expectedId = correctUnitOfMeasuringDTO.Id;
            UnitOfMeasuringDto assert = service.GetById(expectedId);
            Assert.That(assert.Id, Is.EqualTo(expectedId));
        }

        [Test]
        [TestCaseSource(typeof(GettingOkDeletingUnits), nameof(GettingOkDeletingUnits.TestCases))]
        [Category("Get")]
        public void Get_NotExistingIdORDeletedUnit_Exception(Guid id)
        {
            Assert.Throws<EventsExpressException>(() => service.GetById(id));
        }

        [Test]
        [Category("Delete")]
        public void Delete_ExistedUnit_NotThrow()
        {
            Guid expectedId = correctUnitOfMeasuringDTO.Id;
            Assert.DoesNotThrowAsync(() => service.Delete(expectedId));
            Assert.Throws<EventsExpressException>(() => service.GetById(expectedId));
        }

        [Test]
        [Category("Exists By Items")]
        public void Get_ExistingItems_ReturnTrue()
        {
            var res = service.ExistsByItems(unitOfMeasuring.UnitName, unitOfMeasuring.ShortName, unitOfMeasuring.CategoryId);

            Assert.IsTrue(res);
        }

        [Test]
        public void Get_NotExistingByUnitName_ReturnFalse()
        {
            var res = service.ExistsByItems("randomUnitName", unitOfMeasuring.ShortName, unitOfMeasuring.CategoryId);

            Assert.IsFalse(res);
        }

        [Test]
        public void Get_NotExistinByShortName_ReturnFalse()
        {
            var res = service.ExistsByItems(unitOfMeasuring.UnitName, "randomShortName", unitOfMeasuring.CategoryId);

            Assert.IsFalse(res);
        }

        [Test]
        public void Get_NotExistingByCategory_ReturnFalse()
        {
            var res = service.ExistsByItems(unitOfMeasuring.UnitName, unitOfMeasuring.ShortName, Guid.Empty);

            Assert.IsFalse(res);
        }
    }
}
