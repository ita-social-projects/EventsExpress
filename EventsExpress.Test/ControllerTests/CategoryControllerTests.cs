using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    internal class CategoryControllerTests
    {
        private CategoryController _controller;
        private Mock<ICategoryService> _service;
        private IMapper _mapper;

        private CategoryDto testDto;
        private CategoryDto anotherDto;
        private Guid categoryGroupTestId;

        [SetUp]
        public void Initialize()
        {
            _mapper = new MapperConfiguration(config =>
                    config.AddProfile(new CategoryMapperProfile()))
                .CreateMapper();
            _service = new Mock<ICategoryService>();
            _controller = new CategoryController(_service.Object, _mapper);

            categoryGroupTestId = Guid.NewGuid();
            testDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "testCategory",
                CategoryGroupId = categoryGroupTestId,
            };
            anotherDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "anotherCategory",
                CategoryGroupId = categoryGroupTestId,
            };
        }

        private IEnumerable<CategoryDto> GetCategories()
        {
            List<CategoryDto> categories = new List<CategoryDto>()
            {
                testDto,
                anotherDto,
            };
            return categories;
        }

        [Test]
        public void GetAll_OkResult()
        {
            _service.Setup(item => item.GetAllCategories(null))
                     .Returns(GetCategories());

            var response = _controller.All();
            Assert.IsInstanceOf<ObjectResult>(response);

            var data = response as ObjectResult;
            Assert.IsInstanceOf<IEnumerable<CategoryViewModel>>(data.Value);

            var expected = _mapper.Map<IEnumerable<CategoryViewModel>>(GetCategories());
            var actual = data.Value as IEnumerable<CategoryViewModel>;

            CollectionAssert.AreEquivalent(
                expected.Select(i => i.Id).ToList(),
                actual.Select(i => i.Id).ToList());

            CollectionAssert.AreEquivalent(
                expected.Select(i => i.Name).ToList(),
                actual.Select(i => i.Name).ToList());

            CollectionAssert.AreEquivalent(
                expected.Select(i => i.CategoryGroupId).ToList(),
                actual.Select(i => i.CategoryGroupId).ToList());
        }

        [Test]
        public void GetAll_ReturnsStatusOk()
        {
            var res = _controller.All();

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void GetAll_ByCategoryGroupId_OkResult()
        {
            _service.Setup(item => item.GetAllCategories(categoryGroupTestId))
                    .Returns(GetCategories().Where(i =>
                        i.CategoryGroupId == categoryGroupTestId));

            var response = _controller.All(categoryGroupTestId);
            Assert.IsInstanceOf<ObjectResult>(response);

            var data = response as ObjectResult;
            Assert.IsInstanceOf<IEnumerable<CategoryViewModel>>(data.Value);

            var expected = _mapper.Map<IEnumerable<CategoryViewModel>>(
                GetCategories().Where(i => i.CategoryGroupId == categoryGroupTestId));
            var actual = data.Value as IEnumerable<CategoryViewModel>;

            CollectionAssert.AreEquivalent(
                expected.Select(i => i.Id).ToList(),
                actual.Select(i => i.Id).ToList());

            CollectionAssert.AreEquivalent(
                expected.Select(i => i.Name).ToList(),
                actual.Select(i => i.Name).ToList());
        }

        [Test]
        public void GetAll_ByCategoryGroupId_ReturnsStatusOk()
        {
            var res = _controller.All(categoryGroupTestId);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void Create_OkResult()
        {
            var someViewModel = new CategoryCreateViewModel
            {
                Name = "someDto",
                CategoryGroupId = categoryGroupTestId,
            };

            _service.Setup(item => item.Create(someViewModel.Name, someViewModel.CategoryGroupId))
                    .Returns(Task.FromResult(new CategoryDto()));
            var expected = _controller.Create(someViewModel).Result;

            Assert.IsInstanceOf<OkResult>(expected);
        }
    }
}
