using System;
using System.Collections.Generic;
using System.Linq;
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
        private UnitOfMeasuringController unitController;
        private UnitOfMeasuringDto constDTO;
        private UnitOfMeasuringDto newDTO;
        private UnitOfMeasuringDto deletedDTO;
        private UnitOfMeasuringDto otherDTO;

        private Mock<IMapper> MockMapper { get; set; }

        private IEnumerable<UnitOfMeasuringDto> GetUnitsOfMeasuring()
        {
            List<UnitOfMeasuringDto> list = new List<UnitOfMeasuringDto>();
            list.AddRange(new List<UnitOfMeasuringDto>()
            {
                deletedDTO,
                otherDTO,
            });
            return list;
        }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            service = new Mock<IUnitOfMeasuringService>();
            unitController = new UnitOfMeasuringController(service.Object, MockMapper.Object);
            deletedDTO = new UnitOfMeasuringDto()
            {
                Id = Guid.NewGuid(),
                UnitName = "First Unit Name",
                ShortName = "FS/N",
                IsDeleted = false,
            };
            otherDTO = new UnitOfMeasuringDto()
            {
                Id = Guid.NewGuid(),
                UnitName = "Other Unit Name",
                ShortName = "OS/N",
                IsDeleted = true,
            };
            constDTO = new UnitOfMeasuringDto
                        {
                            Id = Guid.NewGuid(),
                            UnitName = "Const Unit name",
                            ShortName = "SN",
                            IsDeleted = false,
                        };
            newDTO = new UnitOfMeasuringDto
                        {
                            Id = Guid.NewGuid(),
                            UnitName = "New Unit Name",
                            ShortName = "NS/N",
                            IsDeleted = false,
                        };

            MockMapper.Setup(u => u.Map<UnitOfMeasuringViewModel, UnitOfMeasuringDto>(It.IsAny<UnitOfMeasuringViewModel>()))
             .Returns((UnitOfMeasuringViewModel e) => e == null ?
             null :
             new UnitOfMeasuringDto
             {
                 Id = e.Id,
                 UnitName = e.UnitName,
                 ShortName = e.ShortName,
                 IsDeleted = false,
             });
            MockMapper.Setup(u => u.Map<UnitOfMeasuringDto, UnitOfMeasuringViewModel>(It.IsAny<UnitOfMeasuringDto>()))
            .Returns((UnitOfMeasuringDto e) => e == null ?
            null :
            new UnitOfMeasuringViewModel
            {
                Id = e.Id,
                UnitName = e.UnitName,
                ShortName = e.ShortName,
            });
        }

        [Test]
        public void GetAll_OkResult()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<UnitOfMeasuringDto>, IEnumerable<UnitOfMeasuringViewModel>>(It.IsAny<IEnumerable<UnitOfMeasuringDto>>()))
            .Returns((IEnumerable<UnitOfMeasuringDto> e) => e.Select(item => new UnitOfMeasuringViewModel { Id = item.Id, UnitName = item.UnitName, ShortName = item.ShortName }));
            service.Setup(item => item.GetAll()).Returns(GetUnitsOfMeasuring());
            var expected = unitController.All();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void GetById_UnknownGuidPassed_ReturnsThrow()
        {
            service.Setup(x => x.GetById(It.IsAny<Guid>()))
                         .Throws<EventsExpressException>();
            Assert.Throws<EventsExpressException>(() => unitController.GetById(Guid.NewGuid()));
        }

        [Test]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            service.Setup(x => x.GetById(It.IsAny<Guid>()))
                         .Returns(new UnitOfMeasuringDto());
            var testGuid = constDTO.Id;
            var okResult = unitController.GetById(testGuid);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Get_RorrectId_ReturnsOkResult()
        {
            service.Setup(x => x.GetById(It.IsAny<Guid>()))
                         .Returns(new UnitOfMeasuringDto
                         {
                             Id = constDTO.Id,
                             UnitName = constDTO.UnitName,
                             ShortName = constDTO.ShortName,
                             IsDeleted = constDTO.IsDeleted,
                         });
            var testGuid = constDTO.Id;
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
            service.Setup(x => x.Create(It.IsAny<UnitOfMeasuringDto>())).Throws<EventsExpressException>();
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Create(null));
        }

        [Test]
        public void Create_CorrectDTO_IdUnit()
        {
            service.Setup(x => x.Create(It.IsAny<UnitOfMeasuringDto>()))
                        .Returns((UnitOfMeasuringDto e) => Task.FromResult(newDTO.Id));

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
        public void Delete_NotExistedUnit_Throw()
        {
            Guid id = Guid.NewGuid();
            service.Setup(x => x.Delete(It.IsAny<Guid>())).Throws<EventsExpressException>();
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Delete(id));
        }

        [Test]
        public void Delete_DeletedUnit_Throw()
        {
            service.Setup(x => x.Delete(It.IsAny<Guid>())).Throws<EventsExpressException>();
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Delete(deletedDTO.Id));
        }

        [Test]
        public void Delete_CorrectUnit_OkResult()
        {
            Guid testId = constDTO.Id;
            service.Setup(x => x.Delete(It.IsAny<Guid>()))
                        .Returns(Task.FromResult(true));
            var okResult = unitController.Delete(testId);
            Assert.IsInstanceOf<OkResult>(okResult.Result);
        }

        [Test]
        public void Edit_NULLUnit_Throw()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Edit(null));
        }

        [Test]
        public void Edit_NotExistedUnit_Throw()
        {
            Guid testId = constDTO.Id;
            service.Setup(x => x.Edit(It.IsAny<UnitOfMeasuringDto>())).Throws<EventsExpressException>();
            UnitOfMeasuringViewModel testItem = new UnitOfMeasuringViewModel()
                            {
                                Id = Guid.NewGuid(),
                                UnitName = "New Unit name",
                                ShortName = "SN",
                            };
            Assert.ThrowsAsync<EventsExpressException>(async () => await unitController.Edit(testItem));
        }

        [Test]
        public void Edit_CorrectView_Id()
        {
            Guid testId = constDTO.Id;
            service.Setup(x => x.Edit(It.IsAny<UnitOfMeasuringDto>()))
                       .Returns(Task.FromResult(testId));
            UnitOfMeasuringViewModel testItem = new UnitOfMeasuringViewModel()
                        {
                            Id = testId,
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
