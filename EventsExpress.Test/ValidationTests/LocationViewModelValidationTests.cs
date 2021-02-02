namespace EventsExpress.Test.ValidationTests
{
    using EventsExpress.Db.Enums;
    using EventsExpress.Test.ValidationTests.TestClasses.Location;
    using EventsExpress.Validation.Base;
    using EventsExpress.ViewModels.Base;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    [TestFixture]
    public class LocationViewModelValidationTests : TestInitializer
    {
        private LocationViewModelValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new LocationViewModelValidator();
        }

        [Test]
        [Category("Latitude is null")]
        public void Should_have_error_when_Latitude_is_null()
        {
            var model = new LocationViewModel { Type = LocationType.Map, Latitude = null, Longitude = 7.7 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Latitude).WithErrorMessage("Field is required!");
        }

        [Test]
        [Category("Longitude is null")]
        public void Should_have_error_when_Longitude_is_null()
        {
            var model = new LocationViewModel { Type = LocationType.Map, Latitude = 8.8, Longitude = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Longitude).WithErrorMessage("Field is required!");
        }

        [TestCaseSource(typeof(CorrectMap))]
        [Category("Correct Longitude and Latitude")]
        public void Should_not_have_error_when_Correct_Map(LocationViewModel model)
        {
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Latitude);
            result.ShouldNotHaveValidationErrorFor(x => x.Longitude);
        }

        [TestCaseSource(typeof(CorrectUrl))]
        [Category("Correct Url")]
        public void Should_not_have_error_when_Correct__URL(LocationViewModel model)
        {
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.OnlineMeeting);
        }

        [TestCaseSource(typeof(InCorrectUrl))]
        [Category("InCorrect Url")]
        [Test]
        public void Should_have_error_when_InCorrect__URL(LocationViewModel model)
        {
            string modelRes = $"Link '{model.OnlineMeeting}' must be a valid URI. eg: http://www.SomeWebSite.com.au";
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OnlineMeeting)
                .WithErrorMessage(modelRes);
        }

        [TestCaseSource(typeof(CorrectEnumViewModel))]
        [Category("Correct Enum")]
        public void SetLocationTypeForEvent_ValidType_ValidationErrorIsNotReturn(LocationViewModel model)
        {
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(e => e.Type);
        }

        [TestCaseSource(typeof(InCorrectEnumViewModel))]
        [Category("InCorrect Enum")]
        public void SetLocationTypeForEvent_InvalidLocation_ReturnError(LocationViewModel model)
        {
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(e => e.Type);
        }
    }
}
