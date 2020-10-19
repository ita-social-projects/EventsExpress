using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;
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

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryService(MockUnitOfWork.Object, MockMapper.Object);
            category = new Category { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName" };
            categoryDTO = new CategoryDTO() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019C"), Name = "RandomName3" };
        }

        [Test]
        public void Get_ExistingId_ReturnEntity()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")))
                .Returns(new Category() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "random" });

            var res = service.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"));

            Assert.IsNotEmpty(res.Name);
        }

        [Test]
        public void Delete_ExistingId_Success()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(category.Id)).
                Returns(category);

            MockUnitOfWork.Setup(u => u.CategoryRepository.Delete(category))
                .Returns(category);

            var res = service.Delete(category.Id);
            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Create_newCategory_Success()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(string.Empty)).
                Returns(new List<Category>()
                    {
                    new Category { Name = "RandomName" },
                    }.AsQueryable);

            var res = service.Create("CorrectName");

            Assert.IsTrue(res.Result.Successed);
        }

        [Test]
        public void Delete_NotExistingId_ReturnFalse()
        {
            Category wrongCategory = new Category() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019B"), };
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(category.Id))
            .Returns(category);

            MockUnitOfWork.Setup(u => u.CategoryRepository.Delete(category))
                .Returns(wrongCategory);

            var res = service.Delete(category.Id);

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Delete_NullId_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(string.Empty));

            var res = service.Delete(default);

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Edit_EmprtyCategory_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(string.Empty));

            var res = service.Edit(new CategoryDTO());

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Create_RepeatTitle_ReturnFalse()
        {
            Category newCategory = new Category() { Name = "RandomName" };
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(string.Empty)).
                Returns(new List<Category>()
                    {
                    new Category { Name = "RandomName" },
                    }.AsQueryable);

            var result = service.Create(newCategory.Name);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Edit_NotExistingId_ReturnFalse()
        {
            CategoryDTO newCategory = new CategoryDTO() { Name = "newName", Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019F") };

            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(string.Empty));
            var result = service.Edit(newCategory);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Edit_ValidDto_Success()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(categoryDTO.Id)).
                Returns(category);
            var result = service.Edit(categoryDTO);
            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Edit_NameExist_ReturnFalse()
        {
            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(string.Empty)).
                Returns(new List<Category>()
                    {
                    new Category { Name = "RandomName" },
                    }.AsQueryable);

            MockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")))
                .Returns(new Category() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019B"), Name = "RandomName2" });

            CategoryDTO newCategoryDto = new CategoryDTO() { Name = "RandomName", Id = category.Id };
            var result = service.Edit(newCategoryDto);
            Assert.IsFalse(result.Result.Successed);
        }
    }
}
