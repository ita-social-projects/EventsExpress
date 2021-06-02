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
        private readonly IPasswordHasher passwordHasher;
        private readonly ISecurityContext securityContext;

        public ChangePasswordViewModelValidator(AppDbContext appDbContext, IPasswordHasher passwordHasher, ISecurityContext securityContext, IAuthService authService)
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
            var userDto = appDbContext.Users
                .Include(u => u.Account)
                    .ThenInclude(a => a.AuthLocal)
                .FirstOrDefault(x => x.Id == securityContext.GetCurrentUserId());
            var authLocal = userDto.Account.AuthLocal;

            return authLocal.PasswordHash == passwordHasher.GenerateHash(password, authLocal.Salt);
        }
    }
}
