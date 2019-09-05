using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework.Internal;
using System.Threading.Tasks;
using EventsExpress.DTO;
using EventsExpress.Core.DTOs;
using System.Linq;

namespace EventsExpress.Test.ServiceTests
{   [TestFixture]
    class CategoryServiceTest: TestInitializer
    {
        private CategoryService service;
        private Category category;
        private CategoryDto categoryDto;
        private CategoryDTO categoryDTO;


        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryService(mockUnitOfWork.Object, mockMapper.Object);
            category = new Category() { Id= new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName"  };

            categoryDto = new CategoryDto() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019A"), Name = "RandomName2" };
            categoryDTO = new CategoryDTO() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019C"), Name = "RandomName3" };

            mockUnitOfWork.Setup(u => u.CategoryRepository
            .Get("")).Returns(new List<Category>()
                {
                    new Category { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "NameIsExist" }
                }
                .AsQueryable());
        }

        [Test]
        public void Get_ExistingId_ReturnEntity()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")))
                .Returns(new Category() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "random" });

            var res = service.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"));

            Assert.IsNotEmpty(res.Name);
        }


        [Test]
        public void Delete_ExistingId_Success()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(category.Id)).
                Returns(category);
            mockUnitOfWork.Setup(u => u.CategoryRepository.Delete(category))
                .Returns(category);

            var res = service.Delete(category.Id);
            Assert.IsTrue(res.Result.Successed);

        }

       

        [Test]
        public void Create_WithEmptyCategoryName_IsFalse()
        {
            var res = service.Create("");

            Assert.IsFalse(res.Result.Successed);
        }

        [Test]
        public void Create_WithNJullCategoryName_IsFalse()
        {
            var res = service.Create(null);

            Assert.IsFalse(res.Result.Successed);
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
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));

            var res = service.Delete(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"));

            Assert.IsFalse(res.Result.Successed);
        }
        [Test]
        public void Delete_NullId_ReturnFalse()
        {
            

            var res = service.Delete(new Guid());

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
            
            
            Category newCategory = new Category() { Name = "NameIsExist" };

            var result = service.Create(newCategory.Name);

            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Edit_NotExistingId_ReturnFalse()
        {
            CategoryDTO newCategory = new CategoryDTO() { Name = "newName", Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019F") };
            var result = service.Edit(newCategory);
            Assert.IsFalse(result.Result.Successed);
        }

        [Test]
        public void Edit_ValidDto_Success()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(categoryDTO.Id)).
                Returns(category);
            var result = service.Edit(categoryDTO);
            Assert.IsTrue(result.Result.Successed);
        }

        [Test]
        public void Edit_NameExist_ReturnFalse()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get("")).
                Returns(new List<Category>()
                    {
                    new Category {Name="RandomName"}
                    }.AsQueryable);

            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")))
                .Returns(new Category() { Id = (new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019B")), Name = "RandomName2" });

            CategoryDTO newCategoryDto = new CategoryDTO() { Name = "RandomName", Id = category.Id };
            var result = service.Edit(newCategoryDto);
            Assert.IsFalse(result.Result.Successed);
        }

        
       
    }
}
