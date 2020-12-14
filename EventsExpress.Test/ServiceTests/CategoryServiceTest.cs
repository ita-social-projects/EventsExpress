using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CategoryServiceTest : TestInitializer
    {
        private CategoryService service;
        private Category category;
        private CategoryDTO categoryDTO;
        private Guid categoryId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryService(Context, MockMapper.Object);
            category = new Category
            {
                Id = categoryId,
                Name = "RandomName",
            };

            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        [Test]
        public void Get_ExistingId_ReturnEntity()
        {
            var res = service.GetById(categoryId);

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
        public void Delete_ExistingId_Success()
        {
            Assert.DoesNotThrowAsync(async () => await service.Delete(category.Id));
        }

        [Test]
        public void Create_newCategory_Success()
        {
            Assert.DoesNotThrowAsync(async () => await service.Create("CorrectName"));
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
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(new CategoryDTO()));
        }

        [Test]
        public void Create_RepeatTitle_ReturnFalse()
        {
            Category newCategory = new Category() { Name = "RandomName" };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(newCategory.Name));
        }

        [Test]
        public void Edit_NotExistingId_ReturnFalse()
        {
            CategoryDTO newCategory = new CategoryDTO()
            {
                Name = "newName",
                Id = Guid.NewGuid(),
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(newCategory));
        }

        [Test]
        public void Edit_ValidDto_Success()
        {
            categoryDTO = new CategoryDTO()
            {
                Id = categoryId,
                Name = "RandomName3",
            };

            Assert.DoesNotThrowAsync(async () => await service.Edit(categoryDTO));
        }

        [Test]
        public void Edit_NameExist_ReturnFalse()
        {
            CategoryDTO newCategoryDto = new CategoryDTO()
            {
                Name = "RandomName",
                Id = category.Id,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(newCategoryDto));
        }
    }
}
