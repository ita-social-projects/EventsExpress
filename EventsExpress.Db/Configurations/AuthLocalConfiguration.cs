using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.EF.Configurations
{
    public class AuthLocalConfiguration : IEntityTypeConfiguration<AuthLocal>
    {
        public void Configure(EntityTypeBuilder<AuthLocal> builder)
        {
            builder.Property(a => a.Salt).HasMaxLength(16);
            builder.Property(a => a.Email).IsRequired();
        }
    }
}
