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
    internal class CategoryOfMeasuringControllerTests
    {
        private Mock<ICategoryOfMeasuringService> service;
        private CategoryOfMeasuringController categoryController;
        private CategoryOfMeasuringDto testDTO;
        private CategoryOfMeasuringDto otherDTO;

        private Mock<IMapper> MockMapper { get; set; }

        private IEnumerable<CategoryOfMeasuringDto> GetCategories()
        {
            List<CategoryOfMeasuringDto> list = new List<CategoryOfMeasuringDto>();
            list.AddRange(new List<CategoryOfMeasuringDto>()
            {
                testDTO,
                otherDTO,
            });
            return list;
        }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            service = new Mock<ICategoryOfMeasuringService>();
            categoryController = new CategoryOfMeasuringController(service.Object, MockMapper.Object);

            testDTO = new CategoryOfMeasuringDto()
            {
                Id = Guid.NewGuid(),
                CategoryName = "Test Name",
            };

            otherDTO = new CategoryOfMeasuringDto()
            {
                Id = Guid.NewGuid(),
                CategoryName = "Other Name",
            };

            MockMapper.Setup(u => u.Map<CategoryOfMeasuringViewModel, CategoryOfMeasuringDto>(It.IsAny<CategoryOfMeasuringViewModel>()))
             .Returns((CategoryOfMeasuringViewModel e) => e == null ?
             null :
             new CategoryOfMeasuringDto
             {
                 Id = e.Id,
                 CategoryName = e.CategoryName,
             });
            MockMapper.Setup(u => u.Map<CategoryOfMeasuringDto, CategoryOfMeasuringViewModel>(It.IsAny<CategoryOfMeasuringDto>()))
            .Returns((CategoryOfMeasuringDto e) => e == null ?
            null :
            new CategoryOfMeasuringViewModel
            {
                Id = e.Id,
                CategoryName = e.CategoryName,
            });
        }

        [Test]
        public void GetAll_OkResult()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<CategoryOfMeasuringDto>, IEnumerable<CategoryOfMeasuringViewModel>>(It.IsAny<IEnumerable<CategoryOfMeasuringDto>>()))
            .Returns((IEnumerable<CategoryOfMeasuringDto> e) => e.Select(item => new CategoryOfMeasuringViewModel { Id = item.Id, CategoryName = item.CategoryName }));
            service.Setup(item => item.GetAllCategories()).Returns(GetCategories());
            var expected = categoryController.GetAll();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void GetAll_ReturnsStatusOK()
        {
            var res = categoryController.GetAll();

            Assert.IsInstanceOf<OkObjectResult>(res);
        }
    }
}
