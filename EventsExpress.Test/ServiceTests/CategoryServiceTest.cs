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

namespace EventsExpress.Test.ServiceTests
{   [TestFixture]
    class CategoryServiceTest: TestInitializer
    {
        private CategoryService service;
        private Category category;
        private CategoryDto categoryDto;


        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryService(mockUnitOfWork.Object, mockMapper.Object);
            category = new Category() { Id= new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName"  };
            categoryDto = new CategoryDto() { Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019A"), Name = "RandomName2" };
        }


       /* [Test]
        public void Delete_NotExistingId_ThrowException()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019A")));
            mockUnitOfWork.Setup(u => u.CategoryRepository.Delete(null));
            mockUnitOfWork.Setup(u =>  u.SaveAsync()).Throws<ArgumentNullException>();
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.Delete(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019A")));
        }*/

        [Test]
        public  void  Delete_ExistingId_Success()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));
            mockUnitOfWork.Setup(u => u.CategoryRepository.Delete(category));
            mockUnitOfWork.Setup(u =>  u.SaveAsync());

            Assert.DoesNotThrowAsync(async  () =>await service.Delete(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));

        }

        [Test]
        public void Create_EmptyCategory_ThrowException()
        {
            mockMapper.Setup(m => m.Map<CategoryDto, Category>(new CategoryDto()))
                .Returns(new Category());
            mockUnitOfWork.Setup(u=>u.CategoryRepository.)
            mockUnitOfWork.Setup(u => u.CategoryRepository.Insert(new Category()));
            mockUnitOfWork.Setup(u => u.SaveAsync());

            Assert.DoesNotThrowAsync(async () => await service.Create(null));
        }

        [Test]
        public void Create_newCategory_Success()
        {
            mockMapper.Setup(m => m.Map<CategoryDto, Category>(categoryDto))
                .Returns(category);
            mockUnitOfWork.Setup(u => u.CategoryRepository.Insert(category));
            mockUnitOfWork.Setup(u => u.SaveAsync());

            Assert.DoesNotThrowAsync(async () => await service.Create("Jor"));
        }

        [Test]
        public void Create_RepeatTitle_ThrowException()
        {
            mockMapper.Setup(m => m.Map<CategoryDto, Category>(categoryDto))
                .Returns(category);
            mockUnitOfWork.Setup(u => u.CategoryRepository.Insert(category));
            mockUnitOfWork.Setup(u => u.SaveAsync());

            Assert.DoesNotThrowAsync(async () => await service.Create("RandomName"));
        }
        
    }
}
