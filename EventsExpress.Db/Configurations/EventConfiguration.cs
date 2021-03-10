using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(u => u.DateFrom)
                .HasColumnType("date");
            builder.Property(u => u.DateTo)
                .HasColumnType("date");
            builder.Property(c => c.MaxParticipants)
                .HasDefaultValue(int.MaxValue);
        }
    }
}
