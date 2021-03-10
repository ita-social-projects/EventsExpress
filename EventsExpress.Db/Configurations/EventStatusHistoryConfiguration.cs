using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class EventStatusHistoryConfiguration : IEntityTypeConfiguration<EventStatusHistory>
    {
        public void Configure(EntityTypeBuilder<EventStatusHistory> builder)
        {
            builder.HasOne(esh => esh.User)
                .WithMany(u => u.ChangedStatusEvents);
            builder.HasOne(esh => esh.Event)
                .WithMany(e => e.StatusHistory);
        }
    }
}
