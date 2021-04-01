using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.EF.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId);

            builder
                .HasOne(a => a.AuthLocal)
                .WithOne(al => al.Account)
                .HasForeignKey<AuthLocal>(al => al.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(a => a.AuthExternal)
                .WithOne(ae => ae.Account)
                .HasForeignKey(ae => ae.AccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
