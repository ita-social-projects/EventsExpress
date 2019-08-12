using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework.Internal;
using System.Threading.Tasks;

namespace EventsExpress.Test.ServiceTests
{   [TestFixture]
    class CategoryServiceTest: TestInitializer
    {
        private CategoryService service;
        private Category category;


        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryService(mockUnitOfWork.Object, mockMapper.Object);
            category = new Category() { Id= new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), Name = "RandomName"  };
        }

        [Test] 
        public void Delete_NotEsistingId_ThrowException()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get( ));
            mockUnitOfWork.Setup(u => u.CategoryRepository.Delete(null));
            mockUnitOfWork.Setup(u => u.SaveAsync()).Throws<NullReferenceException>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.Delete( new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));

        }

        [Test]
        public  void  Delete_ExistingId_Success()
        {
            mockUnitOfWork.Setup(u => u.CategoryRepository.Get(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));
            mockUnitOfWork.Setup(u => u.CategoryRepository.Delete(category));
            mockUnitOfWork.Setup(u =>  u.SaveAsync());

            Assert.DoesNotThrowAsync(async  () =>await service.Delete(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")));

        }

    }
}
