using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;

namespace EventsExpress.ValueResolvers
{
    public class RegisterDtoToAccountResolver : IValueResolver<RegisterDto, Account, AuthLocal>
    {
        private readonly IPasswordHasher passwordHasher;

        public RegisterDtoToAccountResolver(IPasswordHasher passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public AuthLocal Resolve(RegisterDto source, Account destination, AuthLocal destMember, ResolutionContext context)
        {
            var salt = passwordHasher.GenerateSalt();
            return new AuthLocal
            {
                Email = source.Email,
                Salt = salt,
                PasswordHash = passwordHasher.GenerateHash(source.Password, salt),
            };
        }
    }
}
