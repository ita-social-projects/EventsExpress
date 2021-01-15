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
    public class UnitOfMeasuringViewModelValidatorTests : TestInitializer
    {
        private UnitOfMeasuringViewModelValidator unitOfMeasuringViewModelValidator;

        private string existedUnitName = "Existed Unit Name";
        private string existedShortName = "Ex/SN";
        private string notExistedUnitName = "Not Existed Unit Name";
        private string notExistedShortName = "N/SN";
        private const string existedUnitOfMeasuring = "The same UNIT OF MEASURING and SHORT UNIT OF MEASURING already exists!";
        private const string countOfCharactersUnitName = "Unit Name needs to consist of from 5 to 20 characters";
        private const string onlyCharactersUnitName = "Unit name needs to consist only letters or whitespaces";
        private const string countOfCharactersShortName = "Short Name needs to consist of from 1 to 5 characters";
        private const string onlyCharactersShortName = "Short name needs to consist only letters or letter(s)/letter(s)";

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            var mockUnitService = new Mock<IUnitOfMeasuringService>();
            unitOfMeasuringViewModelValidator = new UnitOfMeasuringViewModelValidator(mockUnitService.Object);
            mockUnitService.Setup(x => x.ExistsByName(
             It.Is<string>(i => i == existedUnitName),
             It.Is<string>(i => i == existedShortName))).Returns(true);
        }

        [Test]
        [Category("Existing OR Not Existing Unit Of Measuring")]
        public void ShoudHaveError_ExistingUnitOfMeasuring()
        {
            UnitOfMeasuringViewModel existedModel = new UnitOfMeasuringViewModel
            { Id = Guid.NewGuid(), UnitName = existedUnitName, ShortName = existedShortName };
            var result = unitOfMeasuringViewModelValidator.TestValidate(existedModel);
            result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage(existedUnitOfMeasuring);
        }

        [Test]
        [Category("Existing OR Not Existing Unit Of Measuring")]
        public void ShoudNotHaveError_NotExistingUnitOfMeasuring()
        {
            UnitOfMeasuringViewModel notExistedModel = new UnitOfMeasuringViewModel
            { Id = Guid.NewGuid(), UnitName = notExistedUnitName, ShortName = notExistedShortName };
            var result = unitOfMeasuringViewModelValidator.TestValidate(notExistedModel);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        [TestCaseSource(typeof(CorrectShortName))]
        [Category("Correct Short Name")]
        public void ShoudNotHaveError_CorrectShortName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.ShortName);
        }

        [TestCaseSource(typeof(CorrectUnitName))]
        [Category("Correct Unit Name")]
        public void ShoudNotHaveError_CorrectUnitName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UnitName);
        }

        [TestCaseSource(typeof(InCorrectLengthUnitName))]
        [Category("InCorrect Unit Name")]
        public void ShoudHaveError_InCorrectLengthUnitName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UnitName).WithErrorMessage(countOfCharactersUnitName);
        }

        [TestCaseSource(typeof(LetterAndCharactershUnitName))]
        [Category("InCorrect Unit Name")]
        public void ShoudHaveError_LetterAndCharactersUnitName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UnitName).WithErrorMessage(onlyCharactersUnitName);
        }

        [TestCaseSource(typeof(LittleAndBigCharactershUnitName))]
        [Category("InCorrect Unit Name")]
        public void ShoudHaveError_LittleAndBigCharactersUnitName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UnitName)
                  .WithErrorMessage(countOfCharactersUnitName)
                  .WithErrorMessage(onlyCharactersUnitName);
        }

        [TestCaseSource(typeof(EmptyORManyLettersShortName))]
        [Category("InCorrect Short Name")]
        public void ShoudHaveError_EmptyOrManyLettersShortName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ShortName)
                  .WithErrorMessage(countOfCharactersShortName);
        }

        [TestCaseSource(typeof(DifferentCharactersSlashShortName))]
        [Category("InCorrect Short Name")]
        public void ShoudHaveError_DifferentCharactersSlashShortName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ShortName)
                  .WithErrorMessage(onlyCharactersShortName);
        }

        [TestCaseSource(typeof(DifferentCharactersSlashLengthShortName))]
        [Category("InCorrect Short Name")]
        public void ShoudHaveError_DifferentCharactersSlashLengthShortName(UnitOfMeasuringViewModel model)
        {
            var result = unitOfMeasuringViewModelValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ShortName)
                  .WithErrorMessage(onlyCharactersShortName)
                  .WithErrorMessage(countOfCharactersShortName);
        }
    }
}
