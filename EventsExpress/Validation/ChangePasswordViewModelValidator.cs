using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Validation
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangeViewModel>
    {
        private readonly AppDbContext appDbContext;
        private readonly ISecurityContext securityContext;
        private readonly IPasswordHasher passwordHasher;

        public ChangePasswordViewModelValidator(AppDbContext appDbContext, ISecurityContext securityContext, IPasswordHasher passwordHasher)
        {
            this.appDbContext = appDbContext;
            this.passwordHasher = passwordHasher;
            this.securityContext = securityContext;

            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.OldPassword).Must(ValidOldPassword).WithMessage("Wrong password");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.NewPassword).NotEqual(x => x.OldPassword).WithMessage("New Password can't be the same as Old Password!");
        }

        protected bool ValidOldPassword(string password)
        {
            var authLocal = appDbContext.AuthLocal
                .FirstOrDefault(x => x.AccountId == securityContext.GetCurrentAccountId());

            return authLocal.PasswordHash == passwordHasher.GenerateHash(password, authLocal.Salt);
        }
    }
}
