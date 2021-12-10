using System;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class CategoryMapperProfileTests : MapperTestInitializer<CategoryMapperProfile>
    {
        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void CategoryMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void CategoryMapperProfile_CategoryGroupToCategoryGroupDtoInCategoryToCategoryDto()
        {
            var categoryGroupId = Guid.NewGuid();
            var categoryGroup = new CategoryGroup { Id = categoryGroupId, Title = "Art" };
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, CategoryGroup = categoryGroup };
            var expectedCategoryGroupDto = new CategoryGroupDto { Id = categoryGroupId, Title = "Art" };

            var mappedCategory = Mapper.Map<Category, CategoryDto>(category);
            var mappedCategoryGroupDto = mappedCategory.CategoryGroup;

            Assert.That(mappedCategoryGroupDto.Id, Is.EqualTo(expectedCategoryGroupDto.Id));
            Assert.That(mappedCategoryGroupDto.Title, Is.EqualTo(expectedCategoryGroupDto.Title));
        }
    }
}
