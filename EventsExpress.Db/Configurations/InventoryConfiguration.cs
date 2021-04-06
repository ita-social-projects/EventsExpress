using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasOne(i => i.Event)
                .WithMany(e => e.Inventories)
                .HasForeignKey(i => i.EventId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(i => i.UnitOfMeasuring)
                .WithMany(u => u.Inventories)
                .HasForeignKey(i => i.UnitOfMeasuringId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
