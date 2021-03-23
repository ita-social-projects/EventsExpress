using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.HasKey(t => new { t.UserId, t.EventId });
            builder.HasOne(ue => ue.User)
                .WithMany(u => u.EventsToVisit)
                .HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.Event)
                .WithMany(e => e.Visitors)
                .HasForeignKey(ue => ue.EventId);
        }
    }
}
