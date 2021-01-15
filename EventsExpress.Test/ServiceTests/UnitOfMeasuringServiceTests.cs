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
        private UnitOfMeasuring correctUnitOfMeasuring;
        private UnitOfMeasuring deletedUnitOfMeasuring;
        private UnitOfMeasuringDTO correctUnitOfMeasuringDTO;
        private UnitOfMeasuringDTO deletedUnitOfMeasuringDTO;
        private UnitOfMeasuringDTO unitOfMeasuringDTONotExId;
        private UnitOfMeasuringDTO unitOfMeasuringDTOCreate;
        private Guid correctUnitOfMeasuringId = Guid.NewGuid();
        private Guid deletedUnitOfMeasuringId = new Guid("a1d2dc99-0d30-49e2-b2ee-12411dc461ff");
        private Guid createdId = Guid.NewGuid();
        private Guid notExistedId = new Guid( "e815d623-6b0c-4c85-a847-26a6f5a878b6");
        public string createUnitName = "create Unit Name";
        public string createShortName = "create Unit Name";
        public string correctUnitName =  "CorrectUnitName";
        public string correctShortName = "CSN";
        public string deletedUnitName = "DeletedUnitName";
        public string deletedShortName = "DSN";
        public string randomStringName = "Rnd";
        public string inCorrectUnitName = "78789878Unitk";

        
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

        public void CreateDTO()
        {
            correctUnitOfMeasuringDTO = new UnitOfMeasuringDTO
            {
                Id = correctUnitOfMeasuringId,
                UnitName = correctUnitName,
                ShortName = correctShortName,
                IsDeleted = false,
            };
            unitOfMeasuringDTONotExId = new UnitOfMeasuringDTO
            {
                Id = notExistedId,
                UnitName = correctUnitName,
                ShortName = correctShortName,
                IsDeleted = false,
            };
            deletedUnitOfMeasuringDTO = new UnitOfMeasuringDTO
            {
                Id = deletedUnitOfMeasuringId,
                UnitName = deletedUnitName,
                ShortName = deletedShortName,
                IsDeleted = true,
            };
            unitOfMeasuringDTOCreate = new UnitOfMeasuringDTO
            {
                Id = createdId,
                UnitName = createUnitName,
                ShortName = createShortName,
                IsDeleted = false,
            };

        }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            CreateDTO();
            correctUnitOfMeasuring = FromDTOToUnit(correctUnitOfMeasuringDTO);
            Context.UnitOfMeasurings.Add(correctUnitOfMeasuring);
            Context.SaveChanges();
            deletedUnitOfMeasuring = FromDTOToUnit(deletedUnitOfMeasuringDTO);
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
            UnitOfMeasuringDTO assert = service.GetById(correctUnitOfMeasuring.Id);
            Assert.That(assert.Id, Is.EqualTo(correctUnitOfMeasuring.Id));
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
            Assert.DoesNotThrowAsync(() => service.Delete(correctUnitOfMeasuringId));
            Assert.Throws<EventsExpressException>(() => service.GetById(correctUnitOfMeasuringId));
        }

        [TestCaseSource(typeof(ExistingUnitByName))]
        [Category("Exist By Name")]
        public void ExistsByName_Names_BoolReturned(string expectedUnitName, string expectedShortName, IResolveConstraint constraint)
        { 
            Assert.That(service.ExistsByName(expectedUnitName, expectedShortName), constraint);
        }
    }
}
