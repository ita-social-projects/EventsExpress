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
        private CategoryGroupDto categoryGroupTestDto;

        [SetUp]
        public void Initialize()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryMapperProfile());
                cfg.AddProfile(new CategoryGroupMapperProfile());
            });

            _mapper = mapperConfig.CreateMapper();
            _service = new Mock<ICategoryService>();
            _controller = new CategoryController(_service.Object, _mapper);

            categoryGroupTestDto = new CategoryGroupDto
            {
                Id = Guid.NewGuid(),
                Title = "Test Group",
            };
            testDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "testCategory",
                CategoryGroup = categoryGroupTestDto,
            };
            anotherDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "anotherCategory",
                CategoryGroup = categoryGroupTestDto,
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

            var expected = GetCategories();
            var actual = data.Value as IEnumerable<CategoryViewModel>;

            foreach (var item in actual)
            {
                Guid id = item.Id;
                string name = item.Name;
                Guid groupId = item.CategoryGroup.Id;

                var expectedItem = expected.First(item => item.Id == id);
                Assert.AreEqual(expectedItem.Name, name);
                Assert.AreEqual(expectedItem.CategoryGroup.Id, groupId);
            }
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
            _service.Setup(item => item.GetAllCategories(categoryGroupTestDto.Id))
                    .Returns(GetCategories().Where(i =>
                        i.CategoryGroup.Id == categoryGroupTestDto.Id));

            var response = _controller.All(categoryGroupTestDto.Id);
            Assert.IsInstanceOf<ObjectResult>(response);

            var data = response as ObjectResult;
            Assert.IsInstanceOf<IEnumerable<CategoryViewModel>>(data.Value);

            var expected = GetCategories().Where(i => i.CategoryGroup.Id == categoryGroupTestDto.Id);
            var actual = data.Value as IEnumerable<CategoryViewModel>;

            foreach (var item in actual)
            {
                Guid id = item.Id;
                string name = item.Name;
                Guid groupId = item.CategoryGroup.Id;

                var expectedItem = expected.First(item => item.Id == id);
                Assert.AreEqual(expectedItem.Name, name);
                Assert.AreEqual(expectedItem.CategoryGroup.Id, groupId);
            }
        }

        [Test]
        public void GetAll_ByCategoryGroupId_ReturnsStatusOk()
        {
            var res = _controller.All(categoryGroupTestDto.Id);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void Create_OkResult()
        {
            var someViewModel = new CategoryCreateViewModel
            {
                Name = "someDto",
                CategoryGroup = _mapper.Map<CategoryGroupDto, CategoryGroupViewModel>(categoryGroupTestDto),
            };
            var someDto = _mapper.Map<CategoryCreateViewModel, CategoryDto>(someViewModel);

            _service.Setup(item => item.Create(someDto))
                    .Returns(Task.FromResult(new CategoryDto()));
            var expected = _controller.Create(someViewModel).Result;

            Assert.IsInstanceOf<OkResult>(expected);
        }
    }
}
