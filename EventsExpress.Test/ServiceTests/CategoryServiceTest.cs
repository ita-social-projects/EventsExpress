using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CategoryServiceTest : TestInitializer
    {
        private CategoryService service;
        private Category category;
        private CategoryGroup categoryGroup;
        private CategoryDto categoryDto;
        private CategoryGroupDto categoryGroupDto;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryService(Context, MockMapper.Object);

            categoryGroup = new CategoryGroup { Id = Guid.NewGuid(), Title = "RandomGroup" };
            categoryGroupDto = new CategoryGroupDto { Id = Guid.NewGuid(), Title = "AnotherGroup" };
            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "RandomName",
                CategoryGroup = categoryGroup,
            };
            categoryDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "AnotherName",
                CategoryGroup = categoryGroupDto,
            };

            MockMapper.Setup(m => m.Map<CategoryGroup, CategoryGroupDto>(categoryGroup))
                .Returns(new CategoryGroupDto { Id = categoryGroup.Id, Title = categoryGroup.Title });

            MockMapper.Setup(m => m.Map<Category, CategoryDto>(category))
                .Returns(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    CategoryGroup = MockMapper.Object.Map<CategoryGroup, CategoryGroupDto>(categoryGroup),
                });

            MockMapper.Setup(m => m.Map<CategoryGroupDto, CategoryGroup>(categoryGroupDto))
                .Returns(new CategoryGroup { Id = categoryGroupDto.Id, Title = categoryGroupDto.Title });

            MockMapper.Setup(m => m.Map<CategoryDto, Category>(categoryDto))
                .Returns(new Category
                {
                    Id = categoryDto.Id,
                    Name = categoryDto.Name,
                    CategoryGroup = MockMapper.Object.Map<CategoryGroupDto, CategoryGroup>(categoryGroupDto),
                });

            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        [Test]
        public void Get_ExistingId_ReturnEntity()
        {
            var res = service.GetById(category.Id);

            Assert.IsNotEmpty(res.Name);
        }

        [Test]
        public void Get_ExistingName_ReturnTrue()
        {
            var res = service.ExistsByName(category.Name);

            Assert.IsTrue(res);
        }

        [Test]
        public void Get_NotExistingName_ReturnFalse()
        {
            var res = service.ExistsByName("Category");

            Assert.IsFalse(res);
        }

        [Test]
        public void Get_DuplicateCategory_ReturnTrue()
        {
            var test = MockMapper.Object.Map<Category, CategoryDto>(category);
            var res = service.IsDuplicate(test);

            Assert.IsTrue(res);
        }

        [Test]
        public void Get_NonDuplicateCategory_ReturnFalse()
        {
            var test = new CategoryDto
            {
                Id = Guid.NewGuid(),
                CategoryGroup = MockMapper.Object.Map<CategoryGroup, CategoryGroupDto>(categoryGroup),
            };
            var res = service.IsDuplicate(test);

            Assert.IsFalse(res);
        }

        [Test]
        public void Delete_ExistingId_Success()
        {
            Assert.DoesNotThrowAsync(async () => await service.Delete(category.Id));
        }

        [Test]
        public void Create_newCategory_Success()
        {
            categoryDto.Name = "CorrectName";
            Assert.DoesNotThrowAsync(async () => await service.Create(categoryDto));
        }

        [Test]
        public void Delete_NotExistingId_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Delete(Guid.NewGuid()));
        }

        [Test]
        public void Delete_NullId_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Delete(default));
        }

        [Test]
        public void Edit_EmprtyCategory_ReturnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(new CategoryDto()));
        }

        [Test]
        public void Create_RepeatTitle_ReturnFalseAsync()
        {
            categoryDto.Name = "RandomName";
            Assert.DoesNotThrowAsync(async () => await service.Create(categoryDto));
        }

        [Test]
        public void Edit_NotExistingId_ReturnFalse()
        {
            CategoryDto newCategory = new CategoryDto()
            {
                Name = "newName",
                Id = Guid.NewGuid(),
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(newCategory));
        }

        [Test]
        public void Edit_ValidDto_Success()
        {
            categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = "RandomName3",
            };

            Assert.DoesNotThrowAsync(async () => await service.Edit(categoryDto));
        }

        [Test]
        public void Edit_NameExist_ReturnFalse()
        {
            CategoryDto newCategoryDto = new CategoryDto()
            {
                Name = "RandomName",
                Id = category.Id,
            };

            Assert.DoesNotThrowAsync(async () => await service.Edit(newCategoryDto));
        }
    }
}
