using System;
using EventsExpress.Core.IServices;
using EventsExpress.Test.ValidatorTests.TestClasses.CategoryGroup;
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

            foreach (var group in CorrectCategoryGroup.Groups)
            {
                _categoryGroupService.Setup(service => service.Exists(group.Id))
                                     .Returns(true);
            }
        }

        [TestCaseSource(typeof(CorrectCategoryGroup))]
        public void CategoryGroupId_NotNullOrExisting_IsValid(CategoryGroupViewModel group)
        {
            // Arrange
            CategoryCreateViewModel model = new CategoryCreateViewModel
            {
                Name = "Something",
                CategoryGroup = group,
            };

            // Act
            var res = _validator.TestValidate(model);

            // Assert
            res.ShouldNotHaveValidationErrorFor(m => m.CategoryGroup);
        }

        [TestCaseSource(typeof(IncorrectCategoryGroup))]
        public void CategoryGroupId_NotExisting_ValidationError(CategoryGroupViewModel group)
        {
            // Arrange
            CategoryCreateViewModel model = new CategoryCreateViewModel
            {
                Name = "Something",
                CategoryGroup = group,
            };

            // Act
            var res = _validator.TestValidate(model);

            // Assert
            res.ShouldHaveValidationErrorFor(m => m.CategoryGroup);
        }
    }
}
