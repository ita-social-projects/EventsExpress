using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class ContactAdminConfiguration : IEntityTypeConfiguration<ContactAdmin>
    {
        public void Configure(EntityTypeBuilder<ContactAdmin> builder)
        {
            builder.Property(c => c.Email)
                .IsRequired();
        }
    }
}
