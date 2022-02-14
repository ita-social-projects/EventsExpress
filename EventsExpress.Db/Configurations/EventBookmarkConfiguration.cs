using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configurations;

public class EventBookmarkConfiguration : IEntityTypeConfiguration<EventBookmark>
{
    public void Configure(EntityTypeBuilder<EventBookmark> builder)
    {
        builder.HasOne(r => r.UserFrom)
            .WithMany(u => u.EventBookmarks)
            .HasForeignKey(r => r.UserFromId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(r => r.Event)
            .WithMany(e => e.EventBookmarks)
            .HasForeignKey(r => r.EventId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
