using EventsExpress.Db.Entities;
using HotChocolate.Types;

namespace EventsExpress.Core.GraphQL.Types
{
    public class AccountType : ObjectType<Account>
    {
        protected override void Configure(IObjectTypeDescriptor<Account> descriptor)
        {
            descriptor.Field(f => f.UserId).Type<IdType>();
            descriptor.Field(f => f.IsBlocked).Type<BooleanType>();
            descriptor.Field(f => f.User).Type<UserType>();

            descriptor.Field(f => f.AuthLocal).Type<StringType>();
            descriptor.Field(f => f.AccountRoles).Type<StringType>();
            descriptor.Field(f => f.RefreshTokens).Type<StringType>();
        }
    }
}
