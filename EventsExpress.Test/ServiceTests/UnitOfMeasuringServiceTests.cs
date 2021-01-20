using System;
using System.Collections;
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
        private UnitOfMeasuringService service;
        private UnitOfMeasuringDTO correctUnitOfMeasuringDTO;
        public const string CreateUnitName = "create Unit Name";
        public const string CreateShortName = "create Unit Name";
        public const string CorrectUnitName = "CorrectUnitName";
        public const string CorrectShortName = "CSN";
        public const string DeletedUnitName = "DeletedUnitName";
        public const string DeletedShortName = "DSN";
        public const string RandomStringName = "Rnd";
        public const string InCorrectUnitName = "78789878Unitk";

        public UnitOfMeasuring FromDTOToUnit(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            return new UnitOfMeasuring
            {
                Id = unitOfMeasuringDTO.Id,
                UnitName = unitOfMeasuringDTO.UnitName,
                ShortName = unitOfMeasuringDTO.ShortName,
                IsDeleted = unitOfMeasuringDTO.IsDeleted,
            };
        }

        public void InitTestData()
        {
            correctUnitOfMeasuringDTO = new UnitOfMeasuringDTO
            {
                Id = Guid.NewGuid(),
                UnitName = CorrectUnitName,
                ShortName = CorrectShortName,
                IsDeleted = false,
            };
        }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            InitTestData();
            UnitOfMeasuring correctUnitOfMeasuring = FromDTOToUnit(correctUnitOfMeasuringDTO);
            Context.UnitOfMeasurings.Add(correctUnitOfMeasuring);
            Context.SaveChanges();
            UnitOfMeasuring deletedUnitOfMeasuring = FromDTOToUnit(EditingUnit.DeletedUnitOfMeasuringDTO);
            Context.UnitOfMeasurings.Add(deletedUnitOfMeasuring);
            Context.SaveChanges();
            service = new UnitOfMeasuringService(Context, MockMapper.Object);
            MockMapper.Setup(u => u.Map<UnitOfMeasuringDTO, UnitOfMeasuring>(It.IsAny<UnitOfMeasuringDTO>()))
              .Returns((UnitOfMeasuringDTO e) => e == null ?
              null :
              new UnitOfMeasuring
              {
                  Id = e.Id,
                  UnitName = e.UnitName,
                  ShortName = e.ShortName,
                  IsDeleted = e.IsDeleted,
              });

            MockMapper.Setup(u => u.Map<IEnumerable<UnitOfMeasuring>, IEnumerable<UnitOfMeasuringDTO>>(It.IsAny<IEnumerable<UnitOfMeasuring>>()))
             .Returns((IEnumerable<UnitOfMeasuring> e) => e?.Select(item => new UnitOfMeasuringDTO { Id = item.Id }));
        }

        [Test]
        [Category("Get All")]
        public void Get_ALL_CorrectData()
        {
            int expectedCount = 1;
            var res = service.GetAll();
            Assert.That(res.Any(item => item.Id == correctUnitOfMeasuringDTO.Id), Is.True);
            Assert.That(res.Count(), Is.EqualTo(expectedCount));
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
           UnitOfMeasuringDTO unitOfMeasuringDTOCreate = new UnitOfMeasuringDTO
            {
                Id = Guid.NewGuid(),
                UnitName = CreateUnitName,
                ShortName = CreateShortName,
                IsDeleted = false,
            };
           Guid unitId = await service.Create(unitOfMeasuringDTOCreate);
           Assert.That(unitId, Is.Not.Null);
        }

        [TestCaseSource(typeof(EditingUnit))]
        [Category("Edit")]
        public void Edit_NotExistingIdORDeletedUnit_Exception(UnitOfMeasuringDTO unitType)
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
            UnitOfMeasuringDTO assert = service.GetById(expectedId);
            Assert.That(assert.Id, Is.EqualTo(expectedId));
        }

        [TestCaseSource(typeof(GettingOkDeletingUnits))]
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

        [TestCaseSource(typeof(ExistingUnitByName))]
        [Category("Exist By Name")]
        public void ExistsByName_Names_BoolReturned(string expectedUnitName, string expectedShortName, IResolveConstraint constraint)
        {
            Assert.That(service.ExistsByName(expectedUnitName, expectedShortName), constraint);
        }
    }
}
