using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Mapping;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class CategoryGroupControllerTests
    {
        private CategoryGroupController _controller;
        private Mock<ICategoryGroupService> _service;
        private IMapper _mapper;

        private CategoryGroupDto testDto;
        private CategoryGroupDto anotherDto;

        [SetUp]
        public void Initialize()
        {
            _mapper = new MapperConfiguration(config =>
                    config.AddProfile(new CategoryGroupMapperProfile()))
                .CreateMapper();
            _service = new Mock<ICategoryGroupService>();
            _controller = new CategoryGroupController(_service.Object, _mapper);

            testDto = new CategoryGroupDto
            {
                Id = Guid.NewGuid(),
                Title = "testGroup",
            };
            anotherDto = new CategoryGroupDto
            {
                Id = Guid.NewGuid(),
                Title = "anotherGroup",
            };
        }

        private IEnumerable<CategoryGroupDto> GetCategoryGroups()
        {
            List<CategoryGroupDto> groups = new List<CategoryGroupDto>
            {
                testDto,
                anotherDto,
            };
            return groups;
        }

        [Test]
        public void GetAll_OkResult()
        {
            _service.Setup(item => item.GetAllGroups())
                    .Returns(GetCategoryGroups());

            var expected = (_controller.All() as ObjectResult)
                    .Value as IEnumerable<CategoryGroupViewModel>;

            Assert.AreEqual(expected.Count(), GetCategoryGroups().Count());
        }

        [Test]
        public void GetAll_ReturnsStatusOk()
        {
            var res = _controller.All();

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void GetById_OkResult()
        {
            _service.Setup(item => item.GetById(testDto.Id))
                    .Returns(testDto);

            var expected = (_controller.Get(testDto.Id) as ObjectResult)
                    .Value as CategoryGroupViewModel;

            Assert.AreEqual(expected.Id, testDto.Id);
        }

        [Test]
        public void GetById_ReturnsOk()
        {
            var res = _controller.Get(testDto.Id);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }
    }
}
