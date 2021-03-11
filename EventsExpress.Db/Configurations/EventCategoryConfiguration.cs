using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.HasKey(t => new { t.EventId, t.CategoryId });
            builder.HasOne(ec => ec.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(ec => ec.EventId);
            builder.HasOne(ec => ec.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(uc => uc.CategoryId);
        }
    }
}
