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
    internal class CategoryGroupServiceTests : TestInitializer
    {
        private CategoryGroupService service;
        private CategoryGroup categoryGroup;
        private Guid categoryGroupId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CategoryGroupService(Context, MockMapper.Object);
            categoryGroup = new CategoryGroup
            {
                Id = categoryGroupId,
                Title = "RandomGroupTitle",
            };

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
        public void Get_ExistingName_ReturnTrue()
        {
            var res = service.ExistsByTitle(categoryGroup.Title);

            Assert.IsTrue(res);
        }

        [Test]
        public void Get_NotExistingName_ReturnFalse()
        {
            var res = service.ExistsByTitle("CategoryGroup");

            Assert.IsFalse(res);
        }
    }
}
