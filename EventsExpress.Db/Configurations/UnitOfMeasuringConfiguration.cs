using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class UnitOfMeasuringConfiguration : IEntityTypeConfiguration<UnitOfMeasuring>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasuring> builder)
        {
            builder.HasIndex(u => new { u.UnitName, u.ShortName })
                .HasFilter("IsDeleted = 0")
                .IsUnique();
        }
    }
}
