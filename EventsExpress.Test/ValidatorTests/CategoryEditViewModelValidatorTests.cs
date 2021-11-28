using System;
using AutoMapper;
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
    internal class CategoryEditViewModelValidatorTests
    {
        private CategoryEditViewModel _viewModel;
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

            _viewModel = new CategoryEditViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Something",
                CategoryGroupId = Guid.NewGuid(),
            };

            foreach (Guid id in CorrectId.Ids)
            {
                _categoryGroupService.Setup(service => service.Exists(id))
                                     .Returns(true);
            }
        }

        [TestCaseSource(typeof(CorrectId))]
        public void CategoryGroupId_Existing_IsValid(Guid groupId)
        {
            // Arrange
            _viewModel.CategoryGroupId = groupId;

            // Act
            var res = _validator.TestValidate(_viewModel);

            // Assert
            res.ShouldNotHaveValidationErrorFor(m => m.CategoryGroupId);
        }

        [TestCaseSource(typeof(IncorrectId))]
        public void CategoryGroupId_NotExisting_ValidationError(Guid groupId)
        {
            // Arrange
            _viewModel.CategoryGroupId = groupId;

            // Act
            var res = _validator.TestValidate(_viewModel);

            // Assert
            res.ShouldHaveValidationErrorFor(m => m.CategoryGroupId);
        }
    }
}
