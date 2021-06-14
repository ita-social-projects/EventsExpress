using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class CategoryOfMeasuringConfiguration : IEntityTypeConfiguration<CategoryOfMeasuring>
    {
        public void Configure(EntityTypeBuilder<CategoryOfMeasuring> builder)
        {
            builder.HasMany(c => c.UnitOfMeasurings)
           .WithOne(u => u.Category)
           .HasForeignKey(u => u.CategoryId);
        }
    }
}
