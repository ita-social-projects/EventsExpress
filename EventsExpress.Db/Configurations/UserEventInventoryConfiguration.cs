using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class UserEventInventoryConfiguration : IEntityTypeConfiguration<UserEventInventory>
    {
        public void Configure(EntityTypeBuilder<UserEventInventory> builder)
        {
            builder.HasKey(t => new { t.InventoryId, t.UserId, t.EventId });
            builder.HasOne(uei => uei.UserEvent)
                .WithMany(ue => ue.Inventories)
                .HasForeignKey(uei => new { uei.UserId, uei.EventId })
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(uei => uei.Inventory)
                .WithMany(i => i.UserEventInventories)
                .HasForeignKey(uei => uei.InventoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
