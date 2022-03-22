using System;
using EventsExpress.Db.Enums;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests;

[TestFixture]
internal class RegisterCompleteViewModelValidatorTests
{
    private IValidator<RegisterCompleteViewModel> validator;
    private RegisterCompleteViewModel model;

    [SetUp]
    protected void Initialize()
    {
        validator = new RegisterCompleteViewModelValidator();
        model = new RegisterCompleteViewModel
        {
            Birthday = new DateTime(2000, 1, 1),
            Email = "correct@mail.com",
            Gender = Gender.Male,
            Phone = "+380123456789",
            FirstName = "Username",
        };
    }

    [Test]
    public void Birthday_CorrectValue_ShouldNotHaveError()
    {
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(m => m.Birthday);
    }

    [Test]
    public void Birthday_DefaultValue_ShouldHaveError()
    {
        model.Birthday = default;

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Birthday).WithErrorMessage("Birthday date is required");
    }

    [Test]
    public void Birthday_ValueOutOfRange_ShouldHaveError()
    {
        model.Birthday = DateTime.Now;

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Birthday).WithErrorMessage("Birthday date is not correct");
    }

    [Test]
    public void Email_CorrectValue_ShouldNotHaveError()
    {
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(m => m.Email);
    }

    [Test]
    public void Email_DefaultValue_ShouldHaveError()
    {
        model.Email = default;

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Email).WithErrorMessage("Email address is required");
    }

    [Test]
    public void Email_IncorrectValue_ShouldHaveError()
    {
        model.Email = "IsNotAnEmailAddress";

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Email).WithErrorMessage("Email address is not correct");
    }

    [Test]
    public void Gender_CorrectValue_ShouldNotHaveError()
    {
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(m => m.Gender);
    }

    [Test]
    public void Gender_NonEnumValue_ShouldHaveError()
    {
        model.Gender = (Gender)4;

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Gender).WithErrorMessage("Gender is not correct");
    }

    [Test]
    public void Phone_CorrectValue_ShouldNotHaveError()
    {
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(m => m.Phone);
    }

    [Test]
    public void Phone_EmptyValue_ShouldHaveError()
    {
        model.Phone = string.Empty;

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Phone).WithErrorMessage("Phone number is required");
    }

    [Test]
    public void Phone_IncorrectValue_ShouldHaveError()
    {
        model.Phone = "IsNotAPhoneNumber";

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Phone).WithErrorMessage("Phone number is not correct");
    }

    [Test]
    public void FirstName_CorrectValue_ShouldNotHaveError()
    {
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(m => m.FirstName);
    }

    [Test]
    public void FirstName_DefaultValue_ShouldHaveError()
    {
        model.FirstName = default;

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.FirstName).WithErrorMessage("First name is required");
    }

    [Test]
    public void FirstName_TooShort_ShouldHaveError()
    {
        model.FirstName = "TS";

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.FirstName).WithErrorMessage("First name is too short");
    }

    [Test]
    public void FirstName_TooLong_ShouldHaveError()
    {
        model.FirstName = "ThereAreTooLongUsernameSoLongThatValidationWillFailed";

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.FirstName).WithErrorMessage("First name is too long");
    }
}
