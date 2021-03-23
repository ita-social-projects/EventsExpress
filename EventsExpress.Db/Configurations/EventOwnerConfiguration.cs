using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class EventOwnerConfiguration : IEntityTypeConfiguration<EventOwner>
    {
        public void Configure(EntityTypeBuilder<EventOwner> builder)
        {
            builder.HasKey(c => new { c.UserId, c.EventId });
            builder.HasOne(ue => ue.User)
                .WithMany(u => u.Events)
                .HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.Event)
                .WithMany(e => e.Owners)
                .HasForeignKey(ue => ue.EventId);
            builder.HasIndex(p => new { p.UserId, p.EventId });
        }
    }
}
