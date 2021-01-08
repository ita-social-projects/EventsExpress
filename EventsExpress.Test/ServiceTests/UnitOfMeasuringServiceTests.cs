namespace EventsExpress.Test.ServiceTests
{
    using EventsExpress.Core.DTOs;
    using EventsExpress.Core.Exceptions;
    using EventsExpress.Core.Services;
    using EventsExpress.Db.Entities;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class UnitOfMeasuringServiceTests : TestInitializer
    {
        private UnitOfMeasuringService service;
        private UnitOfMeasuring unitOfMeasuring;
        private UnitOfMeasuringDTO unitOfMeasuringDTO;
        private Guid unitOfMeasuringId = Guid.NewGuid();
        private List<UnitOfMeasuring> unitsOfMeasuring;

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            Random _random = new Random();
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public UnitOfMeasuring CreateRandomUnit()
        {
            return new UnitOfMeasuring
            {
                Id = new Guid(),
                UnitName = RandomString(8),
                ShortName = RandomString(2),
            };
        }

        public UnitOfMeasuring SaveRandomUnit(UnitOfMeasuring rndUnit)
        {
            Context.UnitOfMeasurings.Add(rndUnit);
            Context.SaveChanges();
            return rndUnit;
        }

        public UnitOfMeasuringDTO CreateRandomDTO()
        {
            return new UnitOfMeasuringDTO
            {
                UnitName = RandomString(8),
                ShortName = RandomString(2),
            };
        }

        public UnitOfMeasuringDTO FromUnitToDTO(UnitOfMeasuring unitOfMeasuring)
        {
            return new UnitOfMeasuringDTO
            {
                Id = unitOfMeasuring.Id,
                UnitName = unitOfMeasuring.UnitName,
                ShortName = unitOfMeasuring.ShortName,
                IsDeleted = unitOfMeasuring.IsDeleted
            };
        }
        public bool EqualsUnits(UnitOfMeasuringDTO unitOfMeasuringDTO, UnitOfMeasuring unitOfMeasuring)
        {
            if (unitOfMeasuring.Id == unitOfMeasuringDTO.Id)
            {
                return true;
            }
            return false;
        }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new UnitOfMeasuringService(Context, MockMapper.Object);
            unitOfMeasuring = new UnitOfMeasuring
            {
                Id = unitOfMeasuringId,
                UnitName = "RandomUnitName",
                ShortName = "RndSN",
                IsDeleted = false

            };
            Context.UnitOfMeasurings.Add(unitOfMeasuring);
            Context.SaveChanges();
            unitsOfMeasuring = new List<UnitOfMeasuring>
            {
                  CreateRandomUnit(),
                  CreateRandomUnit(),
                  CreateRandomUnit(),
            };
            Context.UnitOfMeasurings.AddRange(unitsOfMeasuring);
            Context.SaveChanges();
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
        }
        
        [Test]
        public void Create_NULLUnitOfMeasuring_Exception()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(null));
        }

        [Test]
        public async System.Threading.Tasks.Task Create_CorrectDTO_IdUnit()
        {
            UnitOfMeasuringDTO newUnitOfMeasuring = CreateRandomDTO();
            Guid unitId = await service.Create(newUnitOfMeasuring);
            Assert.That(unitId, Is.Not.Null);
        }

        [Test]
        public void Edit_NotExistingId_Exception()
        {
            UnitOfMeasuringDTO expectedDTO = CreateRandomDTO();
            expectedDTO.Id = new Guid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(expectedDTO));
        }

        [Test]
        public void Edit_IsDeletedUnit_ReturnFalse()
        {
            UnitOfMeasuring rndUnitOfMeasuring = CreateRandomUnit();
            UnitOfMeasuring savedUnit = SaveRandomUnit(rndUnitOfMeasuring);
            savedUnit.IsDeleted = true;
            Context.SaveChanges();
            UnitOfMeasuringDTO expectedDTO = FromUnitToDTO(savedUnit);
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(expectedDTO));
        }

        [Test]
        public async System.Threading.Tasks.Task Edit_ValidDTO_IdUnitOfMeasuring()
        {
            UnitOfMeasuring rndUnitOfMeasuring = CreateRandomUnit();
            UnitOfMeasuring savedUnit = SaveRandomUnit(rndUnitOfMeasuring);
            UnitOfMeasuringDTO expectedDTO = FromUnitToDTO(savedUnit);
            expectedDTO.ShortName = RandomString(5);
            expectedDTO.UnitName = RandomString(5);
            Guid expectedId = rndUnitOfMeasuring.Id;
            Guid actualId = await service.Edit(expectedDTO);
            Assert.That(actualId, Is.EqualTo(expectedId));
        }

        [Test]
        public void Get_NotExistingId_Exception()
        {
            Guid Id = new Guid();
            Assert.Throws<EventsExpressException>(() => service.GetById(Id));
        }

        [Test]
        public void Get_IsDeletedUnit_Exception()
        {
            UnitOfMeasuring unitOfMeasuringRandom = CreateRandomUnit();
            UnitOfMeasuring savedUnitOfMeasuring = SaveRandomUnit(unitOfMeasuringRandom);
            savedUnitOfMeasuring.IsDeleted = true;
            Context.SaveChanges();
            Assert.Throws<EventsExpressException>(() => service.GetById(savedUnitOfMeasuring.Id));
        }

        [Test]
        public void Get_ExistedUnit_Unit()
        {
            UnitOfMeasuring unitOfMeasuringRandom = CreateRandomUnit();
            UnitOfMeasuring savedUnitOfMeasuring = SaveRandomUnit(unitOfMeasuringRandom);
            UnitOfMeasuringDTO assert = service.GetById(savedUnitOfMeasuring.Id);
            Assert.That(assert.Id, Is.EqualTo(savedUnitOfMeasuring.Id));
        }

        [Test]
        public void Delete_ExistedUnit_NotThrow()
        {
            UnitOfMeasuring unitOfMeasuringRandom = CreateRandomUnit();
            UnitOfMeasuring savedUnitOfMeasuring = SaveRandomUnit(unitOfMeasuringRandom);
            Assert.DoesNotThrowAsync(() => service.Delete(savedUnitOfMeasuring.Id));
        }

        [Test]
        public void Delete_NotExistedId_Throw()
        {
            Guid expectedId = new Guid();
            Assert.ThrowsAsync<EventsExpressException>(() => service.Delete(expectedId));
        }

        [Test]
        public void Delete_DeletedUnit_Throw()
        {
            UnitOfMeasuring unitOfMeasuringRandom = CreateRandomUnit();
            UnitOfMeasuring savedUnitOfMeasuring = SaveRandomUnit(unitOfMeasuringRandom);
            savedUnitOfMeasuring.IsDeleted = true;
            Context.SaveChanges();
            Assert.ThrowsAsync<EventsExpressException>(() => service.Delete(savedUnitOfMeasuring.Id));
        }

        [Test]

        public void ExistsByName_ExistedUnit_True()
        {
            UnitOfMeasuring unitOfMeasuringRandom = CreateRandomUnit();
            UnitOfMeasuring savedUnitOfMeasuring = SaveRandomUnit(unitOfMeasuringRandom);
            string expectedUnitName = savedUnitOfMeasuring.UnitName;
            string expectedShortName = savedUnitOfMeasuring.ShortName;
            Assert.That(service.ExistsByName(expectedUnitName, expectedShortName), Is.True);
        }

        [Test]
        public void ExistsByName_NotExistUnitName_False()
        {
            UnitOfMeasuring expected = CreateRandomUnit();
            Assert.That(service.ExistsByName(RandomString(8), expected.ShortName), Is.False);
        }

        [Test]
        public void ExistsByName_NotExistShortName_False()
        {
            UnitOfMeasuring expected = CreateRandomUnit();
            Assert.That(service.ExistsByName(expected.UnitName, RandomString(3)), Is.False);
        }

        [Test]
        public void ExistsByName_DeletedUnit_False()
        {
            UnitOfMeasuring unitOfMeasuringRandom = CreateRandomUnit();
            UnitOfMeasuring savedUnitOfMeasuring = SaveRandomUnit(unitOfMeasuringRandom);
            savedUnitOfMeasuring.IsDeleted = true;
            Context.SaveChanges();
            string expectedUnitName = savedUnitOfMeasuring.UnitName;
            string expectedShortName = savedUnitOfMeasuring.ShortName;
            Assert.That(service.ExistsByName(expectedUnitName, expectedShortName), Is.False);
        }
    }
}
