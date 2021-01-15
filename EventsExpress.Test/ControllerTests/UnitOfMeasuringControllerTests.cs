using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class UnitOfMeasuringControllerTests
    {
        private Mock<IUnitOfMeasuringService> service;

        private static Mock<IMapper> MockMapper { get; set; }

        private UnitOfMeasuringController unitController;

        UnitOfMeasuringDTO constDTO;
        private Guid constId = Guid.NewGuid();

        UnitOfMeasuringDTO newDTO;
        private Guid newId = Guid.NewGuid();

        private IEnumerable<UnitOfMeasuringDTO> GetUnitsOfMeasuring()
        {
            List<UnitOfMeasuringDTO> list = new List<UnitOfMeasuringDTO>();
            list.AddRange(new List<UnitOfMeasuringDTO>()
            {
                 new UnitOfMeasuringDTO()
            {
                Id = Guid.NewGuid(),
                UnitName = "First Unit Name",
                ShortName = "FS/N",
                IsDeleted = false,
            },
                 new UnitOfMeasuringDTO()
            {
                Id = Guid.NewGuid(),
                UnitName = "Other Unit Name",
                ShortName = "OS/N",
                IsDeleted = true,
            },
            });
            return list;
        }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            service = new Mock<IUnitOfMeasuringService>();
            unitController = new UnitOfMeasuringController(service.Object, MockMapper.Object);
            constDTO = new UnitOfMeasuringDTO
                        {
                            Id = constId,
                            UnitName = "Const Unit name",
                            ShortName = "SN",
                            IsDeleted = false,
                        };
            newDTO = new UnitOfMeasuringDTO
                        {
                            Id = newId,
                            UnitName = "New Unit Name",
                            ShortName = "NS/N",
                            IsDeleted = false,
                        };

            service.Setup(x => x.GetById(
             It.IsAny<Guid>()))
             .Returns((Guid e) => e == constId ?
            new UnitOfMeasuringDTO
             {
                 Id = constDTO.Id,
                 UnitName = constDTO.UnitName,
                 ShortName = constDTO.ShortName,
                 IsDeleted = constDTO.IsDeleted,
             }
             :
             throw new EventsExpressException("Not found"));

            unitController = new UnitOfMeasuringController(service.Object, MockMapper.Object);

            unitController = new UnitOfMeasuringController(service.Object, MockMapper.Object);

            MockMapper.Setup(u => u.Map<UnitOfMeasuringViewModel, UnitOfMeasuringDTO>(It.IsAny<UnitOfMeasuringViewModel>()))
             .Returns((UnitOfMeasuringViewModel e) => e == null ?
             null :
             new UnitOfMeasuringDTO
             {
                 Id = e.Id,
                 UnitName = e.UnitName,
                 ShortName = e.ShortName,
                 IsDeleted = false,
             });
            MockMapper.Setup(u => u.Map<UnitOfMeasuringDTO, UnitOfMeasuringViewModel>(It.IsAny<UnitOfMeasuringDTO>()))
            .Returns((UnitOfMeasuringDTO e) => e == null ?
            null :
            new UnitOfMeasuringViewModel
            {
                Id = e.Id,
                UnitName = e.UnitName,
                ShortName = e.ShortName,
            });

        }

        [Test]
        public async Task GetAll_OkResult()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<UnitOfMeasuringDTO>, IEnumerable<UnitOfMeasuringViewModel>>(It.IsAny<IEnumerable<UnitOfMeasuringDTO>>()))
            .Returns((IEnumerable<UnitOfMeasuringDTO> e) => e == null ?
            null :
            e.Select(item => new UnitOfMeasuringViewModel { Id = item.Id, UnitName = item.UnitName, ShortName = item.ShortName }));
            service.Setup(item => item.GetAll()).Returns(GetUnitsOfMeasuring());
            var okResult = unitController.All();
            Assert.IsInstanceOf<OkObjectResult>(okResult);        }

        [Test]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            Assert.Throws<EventsExpressException>(() => unitController.GetById(Guid.NewGuid()));
        }

        [Test]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            var testGuid = constId;
            var okResult = unitController.GetById(testGuid);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var testGuid = constId;
            IActionResult actionResult = unitController.GetById(testGuid);
            OkObjectResult okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            UnitOfMeasuringViewModel actualArea = okResult.Value as UnitOfMeasuringViewModel;
            Assert.IsTrue(testGuid.Equals(actualArea.Id));
        }

        [Test]
        public void Greate_NULL_Throw()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Create(null));
        }

        [Test]
        public void Create_CorrectDTO_IdUnit()
        {
            service.Setup(x => x.Create(
             It.IsAny<UnitOfMeasuringDTO>()))
             .Returns((UnitOfMeasuringDTO e) => e.Id == newId ?
             Task.FromResult(newId)
             :
             throw new EventsExpressException("Not found"));
            UnitOfMeasuringViewModel testItem = new UnitOfMeasuringViewModel()
            {
                Id = newDTO.Id,
                UnitName = newDTO.UnitName,
                ShortName = newDTO.ShortName,
            };
            var result = unitController.Create(testItem);

            OkObjectResult okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            Assert.That(testItem.Id, Is.EqualTo(okResult.Value));
        }

        [Test]
        public void Delete_DeletedUnit_Throw()
        {
            Guid id = Guid.NewGuid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Delete(id));
        }

        [Test]
        public void Delete_CorrectUnit_OkResult()
        {
            service.Setup(x => x.Delete(
            It.IsAny<Guid>()))
            .Returns((Guid e) => e == constId ?
            Task.FromResult(true)
            :
            throw new EventsExpressException("Not found"));
            var okResult = unitController.Delete(constId);
            Assert.IsInstanceOf<OkResult>(okResult.Result);
        }

        [Test]
        public void Edit_InvalidObjectPassed_Throw()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Edit(null));
        }

        [Test]
        public void Edit_NotExistedUnit_Throw()
        {
            service.Setup(x => x.Edit(
           It.IsAny<UnitOfMeasuringDTO>()))
           .Returns((UnitOfMeasuringDTO e) => e.Id == constId ?
           Task.FromResult(constId)
           :
           throw new EventsExpressException("Not found"));
            UnitOfMeasuringViewModel testItem = new UnitOfMeasuringViewModel()
            {
                Id = Guid.NewGuid(),
                UnitName = "New Unit name",
                ShortName = "SN",
            };
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Edit(testItem));
        }

        [Test]
        public async System.Threading.Tasks.Task Edit_CorrectView_Id()
        {
            service.Setup(x => x.Edit(
           It.IsAny<UnitOfMeasuringDTO>()))
           .Returns((UnitOfMeasuringDTO e) => e.Id == constId ?
           Task.FromResult(constId)
           :
           throw new EventsExpressException("Not found"));
            UnitOfMeasuringViewModel testItem = new UnitOfMeasuringViewModel()
            {
                Id = constDTO.Id,
                UnitName = "New Unit name",
                ShortName = "SN",
            };
            var result = unitController.Edit(testItem);

            OkObjectResult okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            Assert.That(testItem.Id, Is.EqualTo(okResult.Value));
        }
    }
}
