using System;
using EventsExpress.Core.IServices;
using EventsExpress.Test.ValidationTests.TestClasses.UnitOfMeasuring;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    public class UnitOfMeasuringCreateViewModelValidatorTests : TestInitializer
    {
        private const string ExistedUnitOfMeasuring = "The same UNIT OF MEASURING already exists!";
        private const string CountOfCharactersUnitName = "Unit Name should consist of from 5 to 20 characters";
        private const string OnlyCharactersUnitName = "Unit name should consist only letters or whitespaces";
        private const string CountOfCharactersShortName = "Short Name should consist of from 1 to 5 characters";
        private const string OnlyCharactersShortName = "Short name should consist only letters";
        private readonly string existedUnitName = "Existed Unit Name";
        private readonly string existedShortName = "Ex/SN";
        private readonly string notExistedUnitName = "Not Existed Unit Name";
        private readonly string notExistedShortName = "N/SN";
        private readonly Guid categoryId = Guid.NewGuid();
        private UnitOfMeasuringCreateViewModel unitViewModel;

        private UnitOfMeasuringCreateViewModelValidator unitOfMeasuringViewModelValidator;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            var mockUnitService = new Mock<IUnitOfMeasuringService>();
            unitOfMeasuringViewModelValidator = new UnitOfMeasuringCreateViewModelValidator(mockUnitService.Object);
            mockUnitService.Setup(x => x.ExistsByItems(
             It.Is<string>(i => i == existedUnitName),
             It.Is<string>(i => i == existedShortName),
             It.Is<Guid>(i => i == categoryId))).Returns(true);

            unitViewModel = new UnitOfMeasuringCreateViewModel
            {
                CategoryId = categoryId,
                ShortName = "testShortName",
                UnitName = "testUnitName",
            };
        }

        [Test]
        [Category("Existing OR Not Existing Unit Of Measuring")]
        public void ShoudHaveError_ExistingUnitOfMeasuring()
        {
            UnitOfMeasuringCreateViewModel existedModel = new UnitOfMeasuringCreateViewModel
            { Id = Guid.NewGuid(), UnitName = existedUnitName, ShortName = existedShortName, CategoryId = categoryId };
            var result = unitOfMeasuringViewModelValidator.TestValidate(existedModel);
            result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage(ExistedUnitOfMeasuring);
        }

        [Test]
        [Category("Existing OR Not Existing Unit Of Measuring")]
        public void ShoudNotHaveError_NotExistingUnitOfMeasuring()
        {
            UnitOfMeasuringCreateViewModel notExistedModel = new UnitOfMeasuringCreateViewModel
            { Id = Guid.NewGuid(), UnitName = notExistedUnitName, ShortName = notExistedShortName, CategoryId = categoryId };
            var result = unitOfMeasuringViewModelValidator.TestValidate(notExistedModel);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        [TestCaseSource(typeof(CorrectShortName))]
        [Category("Correct Short Name")]
        public void ShoudNotHaveError_CorrectShortName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.ShortName);
        }

        [TestCaseSource(typeof(CorrectUnitName))]
        [Category("Correct Unit Name")]
        public void ShoudNotHaveError_CorrectUnitName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UnitName);
        }

        [TestCaseSource(typeof(InCorrectLengthUnitName))]
        [Category("InCorrect Unit Name")]
        public void ShoudHaveError_InCorrectLengthUnitName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UnitName).WithErrorMessage(CountOfCharactersUnitName);
        }

        [TestCaseSource(typeof(LetterAndCharactershUnitName))]
        [Category("InCorrect Unit Name")]
        public void ShoudHaveError_LetterAndCharactersUnitName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UnitName).WithErrorMessage(OnlyCharactersUnitName);
        }

        [TestCaseSource(typeof(LittleAndBigCharactershUnitName))]
        [Category("InCorrect Unit Name")]
        public void ShoudHaveError_LittleAndBigCharactersUnitName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UnitName)
                  .WithErrorMessage(CountOfCharactersUnitName)
                  .WithErrorMessage(OnlyCharactersUnitName);
        }

        [TestCaseSource(typeof(EmptyORManyLettersShortName))]
        [Category("InCorrect Short Name")]
        public void ShoudHaveError_EmptyOrManyLettersShortName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ShortName)
                  .WithErrorMessage(CountOfCharactersShortName);
        }

        [TestCaseSource(typeof(DifferentCharactersSlashShortName))]
        [Category("InCorrect Short Name")]
        public void ShoudHaveError_DifferentCharactersSlashShortName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ShortName)
                  .WithErrorMessage(OnlyCharactersShortName);
        }

        [TestCaseSource(typeof(DifferentCharactersSlashLengthShortName))]
        [Category("InCorrect Short Name")]
        public void ShoudHaveError_DifferentCharactersSlashLengthShortName(UnitOfMeasuringCreateViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ShortName)
                  .WithErrorMessage(OnlyCharactersShortName)
                  .WithErrorMessage(CountOfCharactersShortName);
        }

        [Test]
        [Category("Correct Category")]
        public void SelectCategoriesForUnitOfMeasuring_ValidCategories_ValidationErrorIsNotReturn()
        {
            var mockUnitService = new Mock<IUnitOfMeasuringService>();
            mockUnitService.Setup(service => service.ExistsByItems(existedUnitName, existedShortName, categoryId)).Returns(true);
            var result = unitOfMeasuringViewModelValidator.TestValidate(unitViewModel);
            result.ShouldNotHaveValidationErrorFor(e => e.CategoryId);
        }

        [Test]
        [Category("Category Not Selected")]
        public void SelectCategoriesForUnitOfMeasuring_EmptyCategory_ReturnValidationError()
        {
            var unitViewModelEmptyCategory = new UnitOfMeasuringCreateViewModel
            {
                CategoryId = Guid.Empty,
                ShortName = "testShortName",
                UnitName = "testUnitName",
            };
            var result = unitOfMeasuringViewModelValidator.TestValidate(unitViewModelEmptyCategory);
            result.ShouldHaveValidationErrorFor(e => e.CategoryId)
                .WithErrorMessage("Category should be selected");
        }
    }
}
