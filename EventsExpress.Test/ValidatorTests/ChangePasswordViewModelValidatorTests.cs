using System;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    public class ChangePasswordViewModelValidatorTests
    {
        private ChangeViewModel viewModel;
        private ChangePasswordViewModelValidator validator;
        private Mock<ISecurityContext> mockSecurityContext;
        private Mock<IPasswordHasher> mockPasswordHasher;

        private string oldPassword = "OldPassword";
        private string salt = string.Empty;
        private string passwordHash = string.Empty;

        [SetUp]
        public void Setup()
        {
            ConnectionFactory factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            mockSecurityContext = new Mock<ISecurityContext>();
            mockPasswordHasher = new Mock<IPasswordHasher>();
            validator = new ChangePasswordViewModelValidator(context, mockSecurityContext.Object, mockPasswordHasher.Object);

            salt = mockPasswordHasher.Object.GenerateSalt();
            passwordHash = mockPasswordHasher.Object.GenerateHash(oldPassword, salt);

            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var authLocalId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
            };

            var account = new Account
            {
                Id = accountId,
                UserId = userId,
            };

            var authLocal = new AuthLocal
            {
                Id = authLocalId,
                AccountId = accountId,
                PasswordHash = passwordHash,
                Salt = salt,
            };

            context.Users.Add(user);
            context.Accounts.Add(account);
            context.AuthLocal.Add(authLocal);
            context.SaveChanges();

            mockSecurityContext.Setup(x => x.GetCurrentAccountId()).Returns(accountId);

            viewModel = new ChangeViewModel
            {
                OldPassword = "OldPassword",
                NewPassword = "NewPassword",
            };
        }

        [TestCase("OldPassword")]
        public void OldPassword_Valid_ValidationErrorIsNotReturn(string password)
        {
            viewModel.OldPassword = password;

            var result = validator.TestValidate(viewModel);

            result.ShouldNotHaveValidationErrorFor(e => e.OldPassword);
        }

        [TestCase("SomeOldPassword")]
        public void OldPassword_NotValid_ValidationError(string password)
        {
            mockPasswordHasher.Setup(x => x.GenerateHash(password, salt)).Returns("wronghash");

            viewModel.OldPassword = password;

            var result = validator.TestValidate(viewModel);

            result.ShouldHaveValidationErrorFor(e => e.OldPassword);
        }

        [TestCase("NewPassword")]
        public void NewPassword_NotEqualToOldPassword_ValidationErrorIsNotReturn(string password)
        {
            viewModel.NewPassword = password;

            var result = validator.TestValidate(viewModel);

            result.ShouldNotHaveValidationErrorFor(e => e.NewPassword);
        }

        [TestCase("OldPassword")]
        public void NewPassword_EqualToOldPassword_ValidationError(string password)
        {
            viewModel.NewPassword = password;

            var result = validator.TestValidate(viewModel);

            result.ShouldHaveValidationErrorFor(e => e.NewPassword);
        }
    }
}
