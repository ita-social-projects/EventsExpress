using System.Linq;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
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
            builder.HasQueryFilter(e => e.StatusHistory
                   .OrderBy(h => h.CreatedOn)
                   .Last().EventStatus != EventStatus.Deleted);
        }
    }
}
