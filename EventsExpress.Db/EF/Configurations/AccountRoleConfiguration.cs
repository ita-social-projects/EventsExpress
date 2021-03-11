using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.EF.Configurations
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(a => new { a.AccountId, a.RoleId });
            builder.HasOne(a => a.Account).WithMany(a => a.AccountRoles).HasForeignKey(a => a.AccountId);
            builder.HasOne(a => a.Role).WithMany(r => r.Accounts).HasForeignKey(a => a.RoleId);
        }
    }
}
