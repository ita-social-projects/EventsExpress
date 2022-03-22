using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configurations;

public class EventRelationshipConfiguration : IEntityTypeConfiguration<EventRelationship>
{
    public void Configure(EntityTypeBuilder<EventRelationship> builder)
    {
        builder.Property(r => r.Discriminator).HasDefaultValue("Rate");
        builder.HasDiscriminator(r => r.Discriminator);
        builder.HasIndex(r => new { r.Discriminator, r.UserFromId, r.EventId }).IsUnique();
    }
}
