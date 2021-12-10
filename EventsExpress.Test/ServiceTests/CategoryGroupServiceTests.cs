using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CategoryGroupServiceTests : TestInitializer
    {
        private CategoryGroupService service;
        private CategoryGroup categoryGroup;
        private Guid categoryGroupId;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryGroupService(Context, MockMapper.Object);

            categoryGroupId = Guid.NewGuid();
            categoryGroup = new CategoryGroup
            {
                Id = categoryGroupId,
                Title = "RandomGroupTitle",
            };

            MockMapper.Setup(mapper => mapper.Map<CategoryGroup, CategoryGroupDto>(categoryGroup))
                      .Returns(new CategoryGroupDto { Id = categoryGroup.Id, Title = categoryGroup.Title });

            Context.CategoryGroups.Add(categoryGroup);
            Context.SaveChanges();
        }

        [Test]
        public void Get_ExistingId_ReturnEntity()
        {
            var res = service.GetById(categoryGroupId);

            Assert.IsNotEmpty(res.Title);
        }

        [Test]
        public void Get_ExistingId_ReturnTrue()
        {
            var res = service.Exists(categoryGroupId);

            Assert.IsTrue(res);
        }

        [Test]
        public void Get_NotExistingId_ReturnFalse()
        {
            var res = service.Exists(Guid.NewGuid());

            Assert.IsFalse(res);
        }

        public void GetAll_ReturnNotNullList()
        {
            var res = service.GetAllGroups();

            Assert.IsNotNull(res);
        }
    }
}
