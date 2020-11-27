using System;
using EventsExpress.Core.DTOs;
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
        public void Delete_ExistingId_Success()
        {
            var res = service.Delete(category.Id);
            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Create_newCategory_Success()
        {
            var res = service.Create("CorrectName");

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Delete_NotExistingId_ReturnFalse()
        {
            var res = service.Delete(Guid.NewGuid());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Delete_NullId_ReturnFalse()
        {
            var res = service.Delete(default);

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Edit_EmprtyCategory_ReturnFalse()
        {
            var res = service.Edit(new CategoryDTO());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Create_RepeatTitle_ReturnFalse()
        {
            Category newCategory = new Category() { Name = "RandomName" };

            var result = service.Create(newCategory.Name);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Edit_NotExistingId_ReturnFalse()
        {
            CategoryDTO newCategory = new CategoryDTO()
            {
                Name = "newName",
                Id = Guid.NewGuid(),
            };

            var result = service.Edit(newCategory);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Edit_ValidDto_Success()
        {
            categoryDTO = new CategoryDTO()
            {
                Id = categoryId,
                Name = "RandomName3",
            };

            var result = service.Edit(categoryDTO);
            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Edit_NameExist_ReturnFalse()
        {
            CategoryDTO newCategoryDto = new CategoryDTO()
            {
                Name = "RandomName",
                Id = category.Id,
            };

            var result = service.Edit(newCategoryDto);
            Assert.IsFalse(result.Result.Successed);
        }
    }
}
