using System;
using EventsExpress.Core.IServices;
using EventsExpress.Test.ValidatorTests.TestClasses.Guid;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class CategoryCreateViewModelValidatorTests
    {
        private CategoryCreateViewModelValidator _validator;
        private Mock<ICategoryService> _categoryService;
        private Mock<ICategoryGroupService> _categoryGroupService;

        [SetUp]
        public void Initialize()
        {
            _categoryService = new Mock<ICategoryService>();
            _categoryGroupService = new Mock<ICategoryGroupService>();
            _validator = new CategoryCreateViewModelValidator(_categoryService.Object, _categoryGroupService.Object);

            foreach (Guid id in CorrectId.Ids)
            {
                _categoryGroupService.Setup(service => service.Exists(id))
                                     .Returns(true);
            }
        }

        [TestCaseSource(typeof(CorrectId))]
        public void CategoryGroupId_NotNullOrExisting_IsValid(Guid groupId)
        {
            // Arrange
            CategoryCreateViewModel model = new CategoryCreateViewModel
            {
                Name = "Something",
                CategoryGroupId = groupId,
            };

            // Act
            var res = _validator.TestValidate(model);

            // Assert
            res.ShouldNotHaveValidationErrorFor(m => m.CategoryGroupId);
        }

        [TestCase(null)]
        public void CategoryGroupId_Null_ValidationError(Guid groupId)
        {
            // Arrange
            CategoryCreateViewModel model = new CategoryCreateViewModel
            {
                Name = "Something",
                CategoryGroupId = groupId,
            };

            // Act
            var res = _validator.TestValidate(model);

            // Assert
            res.ShouldHaveValidationErrorFor(m => m.CategoryGroupId);
        }

        [TestCaseSource(typeof(IncorrectId))]
        public void CategoryGroupId_NotExisting_ValidationError(Guid groupId)
        {
            // Arrange
            CategoryCreateViewModel model = new CategoryCreateViewModel
            {
                Name = "Something",
                CategoryGroupId = groupId,
            };

            // Act
            var res = _validator.TestValidate(model);

            // Assert
            res.ShouldHaveValidationErrorFor(m => m.CategoryGroupId);
        }
    }
}
