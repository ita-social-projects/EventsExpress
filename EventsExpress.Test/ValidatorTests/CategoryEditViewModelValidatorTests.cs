using System;
using AutoMapper;
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
    internal class CategoryEditViewModelValidatorTests
    {
        private CategoryEditViewModel _viewModel;
        private CategoryGroupViewModel _categoryGroup;
        private Mock<ICategoryService> _categoryService;
        private Mock<ICategoryGroupService> _categoryGroupService;
        private Mock<IMapper> _mapper;
        private CategoryEditViewModelValidator _validator;

        [SetUp]
        public void Initialize()
        {
            _categoryService = new Mock<ICategoryService>();
            _categoryGroupService = new Mock<ICategoryGroupService>();
            _mapper = new Mock<IMapper>();
            _validator = new CategoryEditViewModelValidator(_categoryService.Object, _categoryGroupService.Object, _mapper.Object);

            _categoryGroup = new CategoryGroupViewModel { Id = Guid.NewGuid(), Title = "Some Group" };

            _viewModel = new CategoryEditViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Something",
                CategoryGroup = _categoryGroup,
            };

            foreach (var item in CorrectCategoryGroup.Groups)
            {
                _categoryGroupService.Setup(service => service.Exists(item.Id))
                                     .Returns(true);
            }
        }

        [TestCaseSource(typeof(CorrectCategoryGroup))]
        public void CategoryGroupId_Existing_IsValid(CategoryGroupViewModel group)
        {
            // Arrange
            _viewModel.CategoryGroup = group;

            // Act
            var res = _validator.TestValidate(_viewModel);

            // Assert
            res.ShouldNotHaveValidationErrorFor(m => m.CategoryGroup);
        }

        [TestCaseSource(typeof(IncorrectCategoryGroup))]
        public void CategoryGroupId_NotExisting_ValidationError(CategoryGroupViewModel group)
        {
            // Arrange
            _viewModel.CategoryGroup = group;

            // Act
            var res = _validator.TestValidate(_viewModel);

            // Assert
            res.ShouldHaveValidationErrorFor(m => m.CategoryGroup);
        }
    }
}
